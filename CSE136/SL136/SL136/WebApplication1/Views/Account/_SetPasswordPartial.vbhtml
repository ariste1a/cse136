@ModelType ManageUserViewModel

<p>
    You do not have a local username/password for this site. Add a local
    account so you can log in without an external login.
</p>

@Using Html.BeginForm("Manage", "Account")
    @Html.AntiForgeryToken()
    @Html.ValidationSummary()

     @<fieldset class="form-horizontal">
        <legend>Create Local Login</legend>
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
            <input type="submit" value="Set password" class="btn" />
        </div>
    </fieldset>
End Using
