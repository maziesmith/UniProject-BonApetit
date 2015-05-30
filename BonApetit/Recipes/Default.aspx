<%@ Page Title="Recipes" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BonApetit.Recipes.Recipes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LoginView runat="server" ID="AdminPanel">
        <RoleGroups>
            <asp:RoleGroup Roles="Administrator">
                <ContentTemplate>
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <asp:HyperLink NavigateUrl="~/Recipes/AddRecipe.aspx" ID="AddRecipeButton" runat="server" CssClass="btn btn-success">
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
        <div class="col-md-2">
            <div class="panel panel-default">
                <div class="panel-heading">Categories</div>
                <div class="panel-body">
                    <ul class="list-unstyled">
                        <li class="item"><a href="">All</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="col-md-8">

            <asp:ListView ID="RecipesView" runat="server" ItemType="BonApetit.Models.Recipe" DataKeyNames="Id" SelectMethod="GetRecipes">
                <EmptyDataTemplate>
                    <div class="alert alert-warning text-center" role="alert">No recipes found.</div>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <div class="col-md-3 text-center">
                            <a href="RecipeDetails.aspx?recipeId=<%#:Item.Id %>">
                                <img class="img-responsive img-thumbnail" src="<%#: string.Format("Images/{0}", Item.Image.ImageUrl) %>" alt="<%#:Item.Image.AltText %>" title="<%#:Item.Name %>" />
                                <h3 class="h5" ID="RecipeTitle"><%#:Item.Name %></h3>
                            </a>
                    </div>
                </ItemTemplate>
            </asp:ListView>

        </div>

        <div class="col-md-2">
            <div class="panel panel-success">
                <div class="panel-heading">Latest recipes</div>
                <div class="panel-body">
                    
                </div>
            </div>
        </div>
    </div>

</asp:Content>
