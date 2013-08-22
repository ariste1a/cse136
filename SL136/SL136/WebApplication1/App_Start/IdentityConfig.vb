Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Helpers
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Runtime.CompilerServices

' For more information on ASP.NET Identity, visit http://go.microsoft.com/fwlink/?LinkId=301863
Public Module IdentityConfig
    Public Const LocalLoginProvider As String = "Local"
    Public Property Secrets As IUserSecretStore
    Public Property Logins As IUserLoginStore
    Public Property Users As IUserStore
    Public Property Roles As IRoleStore

    Public Property RoleClaimType As String
    Public Property UserNameClaimType As String
    Public Property UserIdClaimType As String
    Public Property ClaimsIssuer As String

    Public Sub ConfigureIdentity()
        Dim dbContextCreator = New DbContextFactory(Of IdentityDbContext)()
        Secrets = New EFUserSecretStore(Of UserSecret)(dbContextCreator)
        Logins = New EFUserLoginStore(Of UserLogin)(dbContextCreator)
        Users = New EFUserStore(Of User)(dbContextCreator)
        Roles = New EFRoleStore(Of Role, UserRole)(dbContextCreator)
        RoleClaimType = ClaimsIdentity.DefaultRoleClaimType
        UserIdClaimType = "http://schemas.microsoft.com/aspnet/userid"
        UserNameClaimType = "http://schemas.microsoft.com/aspnet/username"
        ClaimsIssuer = ClaimsIdentity.DefaultIssuer
        AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityConfig.UserIdClaimType
    End Sub

    Public Function RemoveUserIdentityClaims(ByVal claims As IEnumerable(Of Claim)) As IList(Of Claim)
        Dim filteredClaims As List(Of Claim) = New List(Of Claim)()
        For Each c As Claim In claims
            ' Remove any existing name/nameid claims
            If (c.Type <> ClaimTypes.Name And c.Type <> ClaimTypes.NameIdentifier) Then
                filteredClaims.Add(c)
            End If
        Next
        Return filteredClaims
    End Function

    Public Sub AddRoleClaims(ByVal roles As IEnumerable(Of String), ByVal claims As IList(Of Claim))
        For Each role As String In roles
            claims.Add(New Claim(RoleClaimType, role, ClaimsIssuer))
        Next
    End Sub

    Public Sub AddUserIdentityClaims(ByVal userId As String, ByVal displayName As String, ByVal claims As IList(Of Claim))
        claims.Add(New Claim(ClaimTypes.Name, displayName, ClaimsIssuer))
        claims.Add(New Claim(UserIdClaimType, userId, ClaimsIssuer))
        claims.Add(New Claim(UserNameClaimType, displayName, ClaimsIssuer))
    End Sub

    Public Sub SignIn(ByVal context As HttpContextBase, ByVal userClaims As IEnumerable(Of Claim), ByVal isPersistent As Boolean)
        context.SignIn(userClaims, ClaimTypes.Name, RoleClaimType, isPersistent)
    End Sub
End Module

Public Module IdentityExtensions
    <Extension>
    Public Function GetUserName(ByVal identity As IIdentity) As String
        Return identity.Name
    End Function

    <Extension>
    Public Function GetUserId(ByVal identity As IIdentity) As String
        Dim ci As ClaimsIdentity = TryCast(identity, ClaimsIdentity)
        If ci IsNot Nothing Then
            Return ci.FindFirstValue(IdentityConfig.UserIdClaimType)
        End If
        Return String.Empty
    End Function

    <Extension>
    Public Function FindFirstValue(ByVal identity As ClaimsIdentity, claimType As String) As String
        Dim claim As Claim = identity.FindFirst(claimType)
        If claim IsNot Nothing Then
            Return claim.Value
        End If
        Return Nothing
    End Function
End Module

