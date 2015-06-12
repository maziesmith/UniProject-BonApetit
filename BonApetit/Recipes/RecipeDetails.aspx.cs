using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Models;

namespace BonApetit.Recipes
{
    public partial class RecipeDetails : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Recipe recipe = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid recipeId;
            if (Guid.TryParse(Request.QueryString["recipeId"], out recipeId))
            {
                this.recipe = db.Recipes.Find(recipeId);
                this.FavouritesButtons_Load();
            }

            this.SetPageTitle();
        }

        #region Get Data Methods

        public BonApetit.Models.Recipe RecipeView_GetItem()
        {
            return this.recipe;
        }

        public IEnumerable<BonApetit.Models.Ingredient> IngredientsList_GetData()
        {
            return this.recipe.Ingredients;
        }

        public IEnumerable<BonApetit.Models.Recipe> LatestRecipesView_GetData()
        {
            var allRecipes = db.GetRecipes().OrderByDescending(r => r.CreateDate);
            var latestRecipes = allRecipes.Take(3);
            return latestRecipes;
        }

        public IEnumerable<BonApetit.Models.Recipe> SimilarRecipesView_GetData()
        {
            var allRecipes = db.GetRecipes().ToList();
            var similarRecipes = allRecipes
                .Where(r => r.Id != recipe.Id)
                .Where(r => r.Categories.Any(c => recipe.Categories.Any(rc => rc.Name == c.Name))) // Get recipes which have at least one category the current recipe has as well
                .OrderByDescending(r => r.CreateDate)
                .Take(3);

            return similarRecipes;
        }

        #endregion

        protected void RecipeView_DataBound(object sender, EventArgs e)
        {
            if (this.recipe != null && this.recipe.Image != null)
            {
                var imageControl = this.RecipeView.Row.Cells[0].FindControl("RecipeImage") as System.Web.UI.WebControls.Image;
                imageControl.ImageUrl = "~/Recipes/Images/" + this.recipe.Image.ImageUrl;
                imageControl.ToolTip = this.recipe.Image.Title;
                imageControl.AlternateText = this.recipe.Image.Title;
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                db.DeleteRecipe(this.recipe.Id);
                db.SaveChanges();

                Response.Redirect("~/Recipes/");
            }
        }

        private void SetPageTitle()
        {
            if (this.recipe != null)
            {
                this.Title = this.recipe.Name;
            }
            else
            {
                this.Title = "Recipe not found";
            }
        }

        protected void EditLink_DataBinding(object sender, EventArgs e)
        {
            var link = sender as HyperLink;
            link.NavigateUrl = ResolveUrl("~/Recipes/ManageRecipe?recipeId=" + this.recipe.Id.ToString());
        }

        #region Elements

        private TableCell RecipeFormContent
        {
            get
            {
                return this.RecipeView.Row.Cells[0];
            }
        }

        private Button AddToFavouritesButton
        {
            get
            {
                return this.RecipeFormContent.FindControl("AddToFavouritesButton") as Button;
            }
        }

        private Button RemoveFromFavouritesButton
        {
            get
            {
                return this.RecipeFormContent.FindControl("RemoveFromFavouritesButton") as Button;
            }
        }

        #endregion

        #region Favourites buttons

        protected void FavouritesButtons_Load()
        {
            if (Request.IsAuthenticated)
            {
                var user = ClaimsPrincipal.Current;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                var isRecipeFavourite = this.recipe.Users.Any(u => u.Id == userId);

                if (isRecipeFavourite)
                    this.ShowRemoveFromFavouritesButton();
                else
                    this.ShowAddToFavouritesButton();
            }
        }

        protected void AddToFavouritesButton_Click(object sender, EventArgs e)
        {
            var user = ClaimsPrincipal.Current;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.recipe.Users.Add(db.GetUser(userId));
            db.SaveChanges();

            this.ShowRemoveFromFavouritesButton();
        }

        protected void RemoveFromFavouritesButton_Click(object sender, EventArgs e)
        {
            var user = ClaimsPrincipal.Current;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.recipe.Users.Remove(db.GetUser(userId));
            db.SaveChanges();

            this.ShowAddToFavouritesButton();
        }

        private void ShowAddToFavouritesButton()
        {
            this.AddToFavouritesButton.Visible = true;
            this.RemoveFromFavouritesButton.Visible = false;
        }

        private void ShowRemoveFromFavouritesButton()
        {
            this.AddToFavouritesButton.Visible = false;
            this.RemoveFromFavouritesButton.Visible = true;
        }

        #endregion
    }
}