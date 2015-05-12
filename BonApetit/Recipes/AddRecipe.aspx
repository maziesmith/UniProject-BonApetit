<%@ Page Title="Add new recipe" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AddRecipe.aspx.cs" Inherits="BonApetit.Recipes.AddRecipe" %>

<%@ Register TagPrefix="uc" TagName="DynamicTextBox"  Src="~/Controls/Forms/DynamicTextBox.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <section id="recipeForm">
                <div class="horizontal">
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="alert alert-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Name" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                                CssClass="text-danger" ErrorMessage="The name field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label">Description</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Description" TextMode="MultiLine" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Description" CssClass="text-danger" ErrorMessage="The description field is required." />
                        </div>
                    </div>
                    <uc:DynamicTextBox ID="Ingredients" runat="server" Title="Ingredients" />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="PreparationInstructions" CssClass="col-md-2 control-label">Preparation instructions</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="PreparationInstructions" TextMode="MultiLine" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PreparationInstructions" CssClass="text-danger" ErrorMessage="The preparation instructions field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ImageUpload" CssClass="col-md-2 control-label">Image</asp:Label>
                        <div class="col-md-10">   
                            <asp:FileUpload CssClass="filestyle" runat="server" ID="ImageUpload" data-buttonBefore="true" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ImageUpload" CssClass="text-danger" ErrorMessage="The image is required." />                      
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="SaveRecipe" Text="Save" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>

    <%: Scripts.Render("~/bundles/filestyle") %>
</asp:Content>
