<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecipeDetails.aspx.cs" Inherits="BonApetit.Recipes.RecipeDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
        <asp:FormView ID="RecipeView" runat="server" ItemType="BonApetit.Models.Recipe" SelectMethod="RecipeView_GetItem" RenderOuterTable="false" OnDataBound="RecipeView_DataBound">
            <EmptyDataTemplate>
                    <div class="alert alert-danger">The recipe wasn't found.</div>
            </EmptyDataTemplate>

            <ItemTemplate>
                <div class="row">
                    <h1 id="recipeTitle"><%#:Item.Name %></h1>
                </div>
                
                    <asp:LoginView runat="server" ID="AdminPanel">
                        <RoleGroups>
                            <asp:RoleGroup Roles="Administrator">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <div class="btn-group">
                                                    <a href="~/Recipes/AddRecipe.aspx" runat="server" class="btn btn-default">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        Add New
                                                    </a>
                                                    <a href="~/Recipes/ManageRecipe.aspx" runat="server" class="btn btn-default">
                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                        Edit
                                                    </a>
                                                    <button type="button" class="btn btn-default" runat="server" onserverclick="DeleteButton_Click" onclick="Confirm();">
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
                                    </div>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Image ID="RecipeImage" CssClass="thumbnail" runat="server" />

                        <div id="description">
                            <%#:Item.Description %>
                        </div>

                        <div id="ingredients">
                            <h3>Ingredients</h3>
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
                        <div id="instructions">
                            <h3>Instructions</h3>
                            <p>
                                <%#:Item.PrepareInstructions %>
                            </p>
                        </div>   
                    </div>
                    <div class="col-md-4">
                        <asp:Repeater ID="AdditionalRecipesView" runat="server" ItemType="BonApetit.Models.Recipe" SelectMethod="AdditionalRecipesView_GetData">
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
            </ItemTemplate>
        </asp:FormView>

</asp:Content>
