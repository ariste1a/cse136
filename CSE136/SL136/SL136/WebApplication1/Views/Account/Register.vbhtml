@ModelType RegisterViewModel
@Code
    ViewBag.Title = "Register"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title.</h1>
</hgroup>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()
    
    @<fieldset class="form-horizontal">
        <legend>Create a new account.</legend>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.UserName, New With { .class  = "control-label" })
            <div class="controls">
                @Html.TextBoxFor(Function(m) m.UserName)
            </div>
        </div>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.Password, New With { .class  = "control-label" })
            <div class="controls">
                @Html.PasswordFor(Function(m) m.Password)
            </div>
        </div>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With { .class  = "control-label" })
            <div class="controls">
                @Html.PasswordFor(Function(m) m.ConfirmPassword)
            </div>
        </div>
        <div class="form-actions no-color">
            <input type="submit" value="Register" class="btn" />
        </div>
    </fieldset>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
