@ModelType LoginViewModel

@Code
    ViewBag.Title = "Log in"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title.</h1>
</hgroup>
<div class="row-fluid">
    <div class="span6">
        <section id="loginForm">
            @using Html.BeginForm(New With { .ReturnUrl = ViewBag.ReturnUrl })
                @Html.AntiForgeryToken()
                @Html.ValidationSummary(true)
                @<fieldset class="form-horizontal">
                    <legend>Use a local account to log in.</legend>
                    <div class="control-group">
                        @Html.LabelFor(Function(m) m.UserName, New With { .class = "control-label" })
                        <div class="controls">
                            @Html.TextBoxFor(Function(m) m.UserName)
                            @Html.ValidationMessageFor(Function(m) m.UserName, Nothing, New With { .class = "help-inline" })
                        </div>
                    </div>
                    <div class="control-group">
                        @Html.LabelFor(Function(m) m.Password, New With { .class = "control-label" })
                        <div class="controls">
                            @Html.PasswordFor(Function(m) m.Password)
                            @Html.ValidationMessageFor(Function(m) m.Password, Nothing, New With { .class = "help-inline" })
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <label class="checkbox">
                                @Html.CheckBoxFor(Function(m) m.RememberMe)
                                @Html.LabelFor(Function(m) m.RememberMe)
                            </label>
                        </div>
                    </div>
                    <div class="form-actions no-color">
                        <input type="submit" value="Log in" class="btn" />
                    </div>
                </fieldset>
                @<p>
                    @Html.ActionLink("Register", "Register") if you don't have a local account.
                </p>
            End Using
        </section>
    </div>
    <div class="span6">
        <section id="socialLoginForm">
            @Html.Action("ExternalLoginsList", New With { .ReturnUrl = ViewBag.ReturnUrl })
        </section>
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
