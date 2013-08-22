@ModelType ManageUserViewModel

<p>You're logged in as <strong>@ViewBag.UserName</strong>.</p>

@Using Html.BeginForm("Manage", "Account")

    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

    @<fieldset class="form-horizontal">
        <legend>Change Password Form</legend>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.OldPassword, New With { .class = "control-label" })
            <div class="controls">
                @Html.PasswordFor(Function(m) m.OldPassword)
            </div>
        </div>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.NewPassword, New With { .class = "control-label" })
            <div class="controls">
                @Html.PasswordFor(Function(m) m.NewPassword)
            </div>
        </div>
        <div class="control-group">
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With { .class = "control-label" })
            <div class="controls">
                @Html.PasswordFor(Function(m) m.ConfirmPassword)
            </div>
        </div>

         <div class="form-actions no-color">
             <input type="submit" value="Change password" class="btn" />
        </div>
    </fieldset>
End Using
