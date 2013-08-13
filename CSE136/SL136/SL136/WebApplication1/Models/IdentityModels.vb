Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Validation
Imports System.Linq
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

' Modify the User class to add extra user information
Public Class User
    Implements IUser
    Public Sub New()
        Me.New(String.Empty)
    End Sub

    Public Sub New(ByVal userName As String)
        Me.UserName = userName
        Id = Guid.NewGuid().ToString()
    End Sub

    Public Property Id As String Implements IUser.Id

    Public Property UserName As String
End Class

Public Class UserLogin
    Implements IUserLogin

    Public Sub New()

    End Sub

    Public Sub New(ByVal userId As String, ByVal loginProvider As String, ByVal providerKey As String)
        Me.LoginProvider = loginProvider
        Me.ProviderKey = providerKey
        Me.UserId = userId
    End Sub

    <Key, Column(Order:=0)>
    Public Property LoginProvider As String Implements IUserLogin.LoginProvider

    <Key, Column(Order:=1)>
    Public Property ProviderKey As String Implements IUserLogin.ProviderKey

    Public Property UserId As String Implements IUserLogin.UserId
End Class

Public Class UserSecret
    Implements IUserSecret

    Public Sub New()

    End Sub

    Public Sub New(ByVal userName As String, ByVal secret As String)
        Me.UserName = userName
        Me.Secret = secret
    End Sub

    Public Property Secret As String Implements IUserSecret.Secret

    <Key>
    Public Property UserName As String Implements IUserSecret.UserName
End Class

Public Class UserRole
    Implements IUserRole

    <Key, Column(Order:=0)>
    Public Property RoleId As String Implements IUserRole.RoleId

    <Key, Column(Order:=1)>
    Public Property UserId As String Implements IUserRole.UserId
End Class

Public Class Role
    Implements IRole

    Public Sub New()
        Me.New(String.Empty)
    End Sub

    Public Sub New(ByVal roleName As String)
        Id = roleName
    End Sub

    <Key>
    Public Property Id As String Implements IRole.Id
End Class

Public Class IdentityDbContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("DefaultConnection")
    End Sub

    Public Sub New(ByVal nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
    End Sub

    Protected Overrides Function ValidateEntity(entityEntry As DbEntityEntry, items As IDictionary(Of Object, Object)) As DbEntityValidationResult
        If entityEntry.State = EntityState.Added Then
            Dim user As User = TryCast(entityEntry.Entity, User)

            If user IsNot Nothing AndAlso Users.Where(Function(u) u.UserName.ToUpper = user.UserName.ToUpper()).Count() > 0 Then
                Dim result = New DbEntityValidationResult(entityEntry, New List(Of DbValidationError)())
                result.ValidationErrors.Add(New DbValidationError("User", "User name must be unique."))
                Return result
            End If
        End If
        Return MyBase.ValidateEntity(entityEntry, items)
    End Function

    Public Property Users As DbSet(Of User)
    Public Property Secrets As DbSet(Of UserSecret)
    Public Property UserLogins As DbSet(Of UserLogin)
    Public Property Roles As DbSet(Of Role)
    Public Property UserRoles As DbSet(Of UserRole)
End Class

