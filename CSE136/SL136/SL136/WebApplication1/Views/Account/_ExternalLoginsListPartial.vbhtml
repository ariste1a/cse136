@Imports Microsoft.Owin.Security

@ModelType ICollection(Of AuthenticationDescription)

@If Model.Count = 0
    @<div class="message-info">
        <p>There are no external authentication services configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=252166">this article</a>
        for details on setting up this ASP.NET application to support logging in via external services.</p>
    </div>
Else
    @using Html.BeginForm("ExternalLogin", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl })
        @Html.AntiForgeryToken()
        @<fieldset id="socialLoginList">
            <legend>Use another service to log in.</legend>
            <p>
            @For Each p As AuthenticationDescription in Model
                @<button type="submit" class="btn" id="@p.AuthenticationType" name="provider" value="@p.AuthenticationType" title="Log in using your @p.Caption account">@p.AuthenticationType</button>
            Next
            </p>
        </fieldset>
    End Using
End if
