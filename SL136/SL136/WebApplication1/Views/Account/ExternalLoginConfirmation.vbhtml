@ModelType ExternalLoginConfirmationViewModel
@Code
    ViewBag.Title = "Register"
End Code

<hgroup>
    <h2>@ViewBag.Title.</h2>
    <h3>Associate your @Model.LoginProvider account.</h3>
</hgroup>

@Using Html.BeginForm("ExternalLoginConfirmation", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl })
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(true)

    @<fieldset>
        <legend>Association Form</legend>
       <p class="text-success">
            You've successfully authenticated with <strong>@Model.LoginProvider</strong>.
            Please enter a user name for this site below and click the Register button to finish
            logging in.
        </p>
                @Html.LabelFor(Function(m) m.UserName)
                @Html.TextBoxFor(Function(m) m.UserName)
                @Html.ValidationMessageFor(Function(m) m.UserName)
        <br />
        @Html.HiddenFor(Function(m) m.LoginProvider)
        <input type="submit" class="btn" value="Register" />
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
