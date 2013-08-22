@Imports Microsoft.AspNet.Identity

@Code 
    ViewBag.Title = "Manage Account"
End Code

<hgroup>
    <h1>@ViewBag.Title.</h1>
</hgroup>

<p class="text-success">@ViewBag.StatusMessage</p>
<div class="row-fluid">
    <div class="span6">
        @if ViewBag.HasLocalPassword
            @Html.Partial("_ChangePasswordPartial")
        Else
            @Html.Partial("_SetPasswordPartial")
        End If

        <section id="externalLogins">
            @Html.Action("RemoveAccountList")
            @Html.Action("ExternalLoginsList", New With { .ReturnUrl = ViewBag.ReturnUrl })
        </section>
    </div>
</div>
@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
