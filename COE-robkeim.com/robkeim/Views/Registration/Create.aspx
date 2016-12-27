<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Robkeim.ViewModels.RegistrationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    2011 registration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>2011 registration</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/registrationCreate.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Registration</legend>

        <div class="editor-label">
            Name:
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("Person", ViewData["participants"] as SelectList, "Select your name" ) %>
            <%: Html.ValidationMessageFor(model => model.Person) %>
        </div>

        <div class="editor-label">
            Participating?:
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("Participating", ViewData["yesNo"] as SelectList) %>
            <%: Html.ValidationMessageFor(model => model.Participating) %>
        </div>

        <div id="participantInfo">
            <div class="editor-label">
                Address (location to send ornament to):
            </div>
            <div class="editor-field">
                <input type="radio" name="AddressButtons" value="Default1" checked="checked" />Default1<br />
                <input type="radio" name="AddressButtons" value="Default2" />Default2<br />
                <input type="radio" name="AddressButtons" value="Other" />Other<br />
                <div id="Address" style="display:none; margin-top: 10px">
                    <div style="margin-bottom: 10px">
                        Street address: <%: Html.EditorFor(model => model.AddressLine1) %>
                    </div>
                    City/state/zip: <%: Html.EditorFor(model => model.AddressLine2) %>
                </div>
            </div>
        </div>
        <div style="margin-top: 20px">
            <input type="submit" value="Register" onclick="OnFormSubmit(); return true;" />
        </div>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Cancel", "Index") %>
</div>

</asp:Content>
