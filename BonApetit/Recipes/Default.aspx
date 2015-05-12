<%@ Page Title="Recipes" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BonApetit.Recipes.Recipes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LoginView runat="server" ID="AdminPanel">
        <RoleGroups>
            <asp:RoleGroup Roles="Administrator">
                <ContentTemplate>
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <asp:HyperLink NavigateUrl="~/Recipes/AddRecipe.aspx" ID="AddRecipeButton" runat="server" CssClass="btn btn-default">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                    Add Recipe
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>

    <div class="row">
        <asp:ListView ID="RecipesView" runat="server" ItemType="BonApetit.Models.Recipe" DataKeyNames="Id" SelectMethod="GetRecipes">
            <EmptyDataTemplate>
                <span>No data was returned.</span>
            </EmptyDataTemplate>

            <ItemTemplate>
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-body">
                        <a href="RecipeDetails.aspx?recipeId=<%#:Item.Id %>">
                            <img class="thumbnail" src="<%#: string.Format("Images/{0}", Item.Image.ImageUrl) %>" alt="<%#:Item.Image.AltText %>" title="<%#:Item.Name %>" />
                            <h3 ID="RecipeTitle"><%#:Item.Name %></h3>
                        </a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>

</asp:Content>
