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
                        <li class="item"><a href="Default.aspx">All</a></li>
                        <asp:Repeater runat="server" ID="Categories" SelectMethod="Categories_GetData" ItemType="BonApetit.Models.Category">
                            <ItemTemplate>
                                <li class="item"><asp:HyperLink runat="server" OnPreRender="CategoryLink_PreRender" Text="<%#: Item.Name %>"></asp:HyperLink></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>

            <asp:HyperLink OnLoad="FavouritesButton_Load" ID="FavouritesButton" Visible="false" runat="server" Text="Show Favourites" CssClass="btn btn-warning" />
        </div>

        <div class="col-md-8">

            <div class="row">
                <asp:ListView ID="RecipesView" runat="server" ItemType="BonApetit.Models.Recipe" DataKeyNames="Id" SelectMethod="GetRecipes" OnPagePropertiesChanging="RecipesView_PagePropertiesChanging">
                    <EmptyDataTemplate>
                        <div class="alert alert-warning text-center" role="alert">No recipes found.</div>
                    </EmptyDataTemplate>

                    <ItemTemplate>
                        <div class="col-md-3 text-center">
                            <a href="RecipeDetails.aspx?recipeId=<%#:Item.Id %>" title="<%#: Item.Name %>">
                                <img class="img-responsive img-thumbnail" src="<%#: string.Format("Images/{0}", Item.Image.ImageUrl) %>" alt="<%#:Item.Image.AltText %>" />
                                <h3 class="h5" ID="RecipeTitle"><%#:Item.Name %></h3>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>

            <div class="row text-center">
                <asp:DataPager runat="server" PagedControlID="RecipesView" PageSize="4" QueryStringField="id" ID="Pager" Visible="true">
                    <Fields>
                        <%--<asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" />--%>
                        <asp:NumericPagerField ButtonType="Button" NumericButtonCssClass="btn btn-default" CurrentPageLabelCssClass="btn btn-warning" />
                        <%--<asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton = "false" />--%>
                    </Fields>
                </asp:DataPager>
            </div>
        </div>

        <div class="col-md-2">
            <div class="panel panel-success">
                <div class="panel-heading">Latest recipes</div>
                <div class="panel-body">
                    <asp:Repeater ID="LatestRecipes" runat="server" ItemType="BonApetit.Models.Recipe" SelectMethod="LatestRecipes_GetData">
                        <ItemTemplate>
                            <a href="RecipeDetails.aspx?recipeId=<%#:Item.Id %>" title="<%#: Item.Name %>">
                                <img src="<%#: string.Format("Images/{0}", Item.Image.ImageUrl) %>" class="img-responsive img-thumbnail" />
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
