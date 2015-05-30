<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecipeDetails.aspx.cs" Inherits="BonApetit.Recipes.RecipeDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
        <asp:FormView ID="RecipeView" runat="server" ItemType="BonApetit.Models.Recipe" SelectMethod="RecipeView_GetItem" RenderOuterTable="false" OnDataBound="RecipeView_DataBound">
            <EmptyDataTemplate>
                    <div class="alert alert-danger">The recipe wasn't found.</div>
            </EmptyDataTemplate>

            <ItemTemplate>
                <asp:LoginView runat="server" ID="AdminPanel">
                    <RoleGroups>
                        <asp:RoleGroup Roles="Administrator">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                                <%--<a href="~/Recipes/AddRecipe.aspx" runat="server" class="btn btn-default">
                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    Add New
                                                </a>--%>
                                                <asp:HyperLink runat="server" CssClass="btn btn-success" ID="EditLink" OnDataBinding="EditLink_DataBinding">
                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                    Edit
                                                </asp:HyperLink>
                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="DeleteButton_Click" onclick="Confirm();">
                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                    Delete
                                                </button>
                                                <input id="confirmDeleteValue" type="hidden" name="confirm_value" value="No" />

                                                <script type = "text/javascript">
                                                    function Confirm() {
                                                        var confirm_value = document.getElementById("confirmDeleteValue");
                                                        if (confirm("Do you want to save data?")) {
                                                            confirm_value.value = "Yes";
                                                        } else {
                                                            confirm_value.value = "No";
                                                        }
                                                    }
                                                </script>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups>
                </asp:LoginView>

                <div class="row">
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-5">
                                <asp:Image ID="RecipeImage" CssClass="img-thumbnail img-responsive" runat="server" />
                            </div>

                            <div class="col-md-7">
                                <h1 id="recipeTitle" class="h2"><%#:Item.Name %></h1>

                                <div id="description">
                                    <%#:Item.Description %>
                                </div>

                                <div id="ingredients">
                                    <h3 class="h4">Ingredients</h3>
                                    <ul>
                                        <asp:Repeater ID="IngredientsList" ItemType="BonApetit.Models.Ingredient" SelectMethod="IngredientsList_GetData" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <%#:Item.Content %>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="instructions" class="row">
                            <h3 class="h4">Instructions</h3>
                            <p>
                                <%#:Item.PrepareInstructions %>
                            </p>
                        </div>   
                    </div>

                    <div class="col-md-2">
                        <div class="panel panel-warning">
                            <div class="panel-heading">Similar recipes</div>
                            <div class="panel-body">
                                
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="panel panel-success">
                            <div class="panel-heading">Latest recipes</div>
                            <div class="panel-body">
                                <asp:Repeater ID="Repeater1" runat="server" ItemType="BonApetit.Models.Recipe" SelectMethod="AdditionalRecipesView_GetData">
                                    <ItemTemplate>
                                        <div>
                                            <a href="RecipeDetails.aspx?recipeId=<%#:Item.Id %>">
                                                <img src="<%#: string.Format("/Images/{0}", Item.Image.ImageUrl) %>" alt="<%#:Item.Image.AltText %>" title="<%#:Item.Name %>" />
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <asp:HyperLink CssClass="btn btn-primary" Text="Back to Recipes" runat="server" NavigateUrl="~/Recipes/Default.aspx" />
                </div>
            </ItemTemplate>
        </asp:FormView>

</asp:Content>
