Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Entity.Validation
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin.Security

<Authorize>
Public Class AccountController
    Inherits Controller
    Public Sub New()
        Me.New(IdentityConfig.Secrets, IdentityConfig.Logins, IdentityConfig.Users, IdentityConfig.Roles)
    End Sub

    Public Sub New(secrets As IUserSecretStore, logins As IUserLoginStore, users As IUserStore, roles As IRoleStore)
        Me.Secrets = secrets
        Me.Logins = logins
        Me.Users = users
        Me.Roles = roles
    End Sub

    Public Property Secrets As IUserSecretStore
    Public Property Logins As IUserLoginStore
    Public Property Users As IUserStore
    Public Property Roles As IRoleStore

    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        ViewBag.ReturnUrl = returnUrl
        Return View()
    End Function

    '
    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        If ModelState.IsValid Then
            ' Validate the user password
            If Await Secrets.Validate(model.UserName, model.Password) Then
                Dim userId As String = Await Logins.GetUserId(IdentityConfig.LocalLoginProvider, model.UserName)
                Await SignIn(userId, model.RememberMe)
                Return RedirectToLocal(returnUrl)
            End If
        End If

        ' If we got this far, something failed, redisplay form
        ModelState.AddModelError([String].Empty, "The user name or password provided is incorrect.")
        Return View(model)
    End Function

    '
    ' GET: /Account/Register
    <AllowAnonymous>
    Public Function Register() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/Register
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Try
                ' Create a profile, password, and link the local login before signing in the user
                Dim user As New User(model.UserName)
                If Await Users.Create(user) AndAlso Await Secrets.Create(New UserSecret(model.UserName, model.Password)) AndAlso Await Logins.Add(New UserLogin(user.Id, IdentityConfig.LocalLoginProvider, model.UserName)) Then
                    Await SignIn(user.Id, isPersistent:=False)
                    Return RedirectToAction("Index", "Home")
                Else
                    ModelState.AddModelError([String].Empty, "Failed to create login for: " & Convert.ToString(model.UserName))
                End If
            Catch e As DbEntityValidationException
                ModelState.AddModelError([String].Empty, e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage)
            End Try
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' POST: /Account/Disassociate
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function Disassociate(loginProvider As String, providerKey As String) As Task(Of ActionResult)
        Dim message As System.Nullable(Of ManageMessageId) = Nothing
        Dim userId As String = User.Identity.GetUserId()
        If Await UnlinkAccountForUser(userId, loginProvider, providerKey) Then
            ' If you remove a local login, need to delete the login as well
            If loginProvider = IdentityConfig.LocalLoginProvider Then
                Await Secrets.Delete(providerKey)
            End If
            message = ManageMessageId.RemoveLoginSuccess
        End If

        Return RedirectToAction("Manage", New With {
            Key .Message = message
        })
    End Function

    '
    ' GET: /Account/Manage
    Public Async Function Manage(message As System.Nullable(Of ManageMessageId)) As Task(Of ActionResult)
        Select Case message
            Case ManageMessageId.ChangePasswordSuccess
                ViewBag.StatusMessage = "Your password has been changed."
            Case ManageMessageId.SetPasswordSuccess
                ViewBag.StatusMessage = "Your password has been set."
            Case ManageMessageId.RemoveLoginSuccess
                ViewBag.StatusMessage = "The external login was removed."
            Case Else
                ViewBag.StatusMessage = String.Empty
        End Select
        Dim localUserName As String = Await Logins.GetProviderKey(User.Identity.GetUserId(), IdentityConfig.LocalLoginProvider)
        ViewBag.UserName = localUserName
        ViewBag.HasLocalPassword = localUserName IsNot Nothing
        ViewBag.ReturnUrl = Url.Action("Manage")
        Return View()
    End Function

    '
    ' POST: /Account/Manage
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function Manage(model As ManageUserViewModel) As Task(Of ActionResult)
        Dim userId As String = User.Identity.GetUserId()
        Dim localUserName As String = Await Logins.GetProviderKey(User.Identity.GetUserId(), IdentityConfig.LocalLoginProvider)
        Dim hasLocalLogin As Boolean = localUserName IsNot Nothing
        ViewBag.HasLocalPassword = hasLocalLogin
        ViewBag.ReturnUrl = Url.Action("Manage")
        If hasLocalLogin Then
            If ModelState.IsValid Then
                Dim changePasswordSucceeded As Boolean = Await ChangePassword(localUserName, model.OldPassword, model.NewPassword)
                If changePasswordSucceeded Then
                    Return RedirectToAction("Manage", New With {
                        Key .Message = ManageMessageId.ChangePasswordSuccess
                    })
                Else
                    ModelState.AddModelError([String].Empty, "The current password is incorrect or the new password is invalid.")
                End If
            End If
        Else
            ' User does not have a local password so remove any validation errors caused by a missing OldPassword field
            Dim state As ModelState = ModelState("OldPassword")
            If state IsNot Nothing Then
                state.Errors.Clear()
            End If

            If ModelState.IsValid Then
                Try
                    ' Create the local login info and link the local account to the user
                    localUserName = User.Identity.GetUserName()
                    If Await Secrets.Create(New UserSecret(localUserName, model.NewPassword)) AndAlso Await Logins.Add(New UserLogin(userId, IdentityConfig.LocalLoginProvider, localUserName)) Then
                        Return RedirectToAction("Manage", New With {
                            Key .Message = ManageMessageId.SetPasswordSuccess
                        })
                    Else
                        ModelState.AddModelError([String].Empty, "Failed to set password")
                    End If
                Catch e As Exception
                    ModelState.AddModelError([String].Empty, e)
                End Try
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    '
    ' POST: /Account/ExternalLogin
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
        ' Request a redirect to the external login provider
        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {
            Key .loginProvider = provider,
            Key .ReturnUrl = returnUrl
        }))
    End Function

    '
    ' GET: /Account/ExternalLoginCallback
    <AllowAnonymous>
    Public Async Function ExternalLoginCallback(loginProvider As String, returnUrl As String) As Task(Of ActionResult)
        ' Get the information about the user from the external login provider
        Dim id As ClaimsIdentity = Await HttpContext.GetExternalIdentity()
        If id Is Nothing Then
            Return View("ExternalLoginFailure")
        End If

        ' Make sure the external identity is from the loginProvider we expect
        Dim providerKeyClaim As Claim = id.FindFirst(ClaimTypes.NameIdentifier)

        If providerKeyClaim Is Nothing OrElse providerKeyClaim.Issuer <> loginProvider Then
            Return View("ExternalLoginFailure")
        End If

        ' Succeeded so we should be able to lookup the local user name and sign them in
        Dim providerKey As String = providerKeyClaim.Value
            Dim userId As String = Await Logins.GetUserId(loginProvider, providerKey)
            If Not [String].IsNullOrEmpty(userId) Then
                Await SignIn(userId, id.Claims, isPersistent:=False)
            Else
                ' No local user for this account
                If User.Identity.IsAuthenticated Then
                    ' If the current user is logged in, just add the new account
                    Await Logins.Add(New UserLogin(User.Identity.GetUserId(), loginProvider, providerKey))
                Else
                    ViewBag.ReturnUrl = returnUrl
                    Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {
                            .UserName = id.Name,
                            .LoginProvider = loginProvider
                        })
                End If
            End If

            Return RedirectToLocal(returnUrl)
    End Function

    '
    ' POST: /Account/ExternalLoginConfirmation
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
        If User.Identity.IsAuthenticated Then
            Return RedirectToAction("Manage")
        End If

        If ModelState.IsValid Then
            ' Get the information about the user from the external login provider
            Dim id As ClaimsIdentity = Await HttpContext.GetExternalIdentity()
            If id Is Nothing Then
                Return View("ExternalLoginFailure")
            End If
            Try
                ' Create a local user and sign in
                Dim user = New User(model.UserName)
                If Await Users.Create(user) AndAlso Await Logins.Add(New UserLogin(user.Id, model.LoginProvider, id.FindFirstValue(ClaimTypes.NameIdentifier))) Then
                    Await SignIn(user.Id, id.Claims, isPersistent:=False)
                    Return RedirectToLocal(returnUrl)
                Else
                    Return View("ExternalLoginFailure")
                End If
            Catch e As DbEntityValidationException
                ModelState.AddModelError([String].Empty, e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage)
            End Try
        End If

        ViewBag.ReturnUrl = returnUrl
        Return View(model)
    End Function

    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        HttpContext.SignOut()
        Return RedirectToAction("Index", "Home")
    End Function

    '
    ' GET: /Account/ExternalLoginFailure
    <AllowAnonymous>
    Public Function ExternalLoginFailure() As ActionResult
        Return View()
    End Function

    <AllowAnonymous>
    <ChildActionOnly>
    Public Function ExternalLoginsList(returnUrl As String) As ActionResult
        ViewBag.ReturnUrl = returnUrl
        Return DirectCast(PartialView("_ExternalLoginsListPartial", New List(Of AuthenticationDescription)(HttpContext.GetExternalAuthenticationTypes())), ActionResult)
    End Function

    <ChildActionOnly>
    Public Function RemoveAccountList() As ActionResult
        Return Task.Run(Async Function()
                            Dim linkedAccounts = Await Logins.GetLogins(User.Identity.GetUserId())
                            ViewBag.ShowRemoveButton = linkedAccounts.Count > 1
                            Return DirectCast(PartialView("_RemoveAccountPartial", linkedAccounts), ActionResult)

                        End Function).Result
    End Function

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        Else
            Return RedirectToAction("Index", "Home")
        End If
    End Function

    Private Async Function UnlinkAccountForUser(userId As String, loginProvider As String, providerKey As String) As Task(Of Boolean)
        Dim ownerAccount As String = Await Logins.GetUserId(loginProvider, providerKey)
        If ownerAccount = userId Then
            If (Await Logins.GetLogins(userId)).Count > 1 Then
                Await Logins.Remove(userId, loginProvider, providerKey)
                Return True
            End If
        End If
        Return False
    End Function

    Private Async Function ChangePassword(userName As String, oldPassword As String, newPassword As String) As Task(Of Boolean)
        Dim changePasswordSucceeded As Boolean = False
        If Await Secrets.Validate(userName, oldPassword) Then
            changePasswordSucceeded = Await Secrets.UpdateSecret(userName, newPassword)
        End If
        Return changePasswordSucceeded
    End Function

    Private Function SignIn(userId As String, isPersistent As Boolean) As Task
        Return SignIn(userId, New Claim(-1) {}, isPersistent)
    End Function

    Private Async Function SignIn(userId As String, claims As IEnumerable(Of Claim), isPersistent As Boolean) As Task
        Dim user As User = TryCast(Await Users.Find(userId), User)
        If user IsNot Nothing Then
            ' Replace UserIdentity claims with the application specific claims
            Dim userClaims As IList(Of Claim) = IdentityConfig.RemoveUserIdentityClaims(claims)
            IdentityConfig.AddUserIdentityClaims(userId, user.UserName, userClaims)
            IdentityConfig.AddRoleClaims(Await Roles.GetRolesForUser(userId), userClaims)
            IdentityConfig.SignIn(HttpContext, userClaims, isPersistent)
        End If
    End Function

    Private Class ChallengeResult
        Inherits HttpUnauthorizedResult
        Public Sub New(provider As String, redirectUrl As String)
            Me.LoginProvider = provider
            Me.RedirectUrl = redirectUrl
        End Sub

        Public Property LoginProvider As String
        Public Property RedirectUrl As String

        Public Overrides Sub ExecuteResult(context As ControllerContext)
            context.HttpContext.Challenge(LoginProvider, New AuthenticationExtra() With {
                .RedirectUrl = RedirectUrl
            })
        End Sub
    End Class

    Public Enum ManageMessageId
        ChangePasswordSuccess
        SetPasswordSuccess
        RemoveLoginSuccess
    End Enum
End Class
