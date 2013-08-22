@Imports Microsoft.AspNet.Identity
@ModelType ICollection(Of IUserLogin)

@If Model.Count > 0
    @<h3>Registered Logins</h3>
    @<table class="table">
        <tbody>
            @For Each account As IUserLogin in Model
                @<tr>
                    <td>@account.LoginProvider</td>
                    <td>
                        @If ViewBag.ShowRemoveButton Then
                            using Html.BeginForm("Disassociate", "Account")
                                @Html.AntiForgeryToken()
                                @<fieldset>
                                    @Html.Hidden("loginProvider", account.LoginProvider)
                                    @Html.Hidden("providerKey", account.ProviderKey)
                                        <input type="submit" class="btn" value="Remove" title="Remove this @account.LoginProvider login from your account" />
                                </fieldset>
                            End Using
                        Else
                            @: &nbsp;
                        End If
                    </td>
                </tr>
            Next
        </tbody>
    </table>
End If
