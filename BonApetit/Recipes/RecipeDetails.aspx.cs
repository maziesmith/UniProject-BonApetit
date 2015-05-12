using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Models;

namespace BonApetit.Recipes
{
    public partial class RecipeDetails : System.Web.UI.Page
    {
        private Recipe recipe = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid recipeId;
            if (Guid.TryParse(Request.QueryString["recipeId"], out recipeId))
            {
                var _db = new ApplicationDbContext();
                this.recipe = _db.Recipes.Find(recipeId);
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

        public IEnumerable<BonApetit.Models.Recipe> AdditionalRecipesView_GetData()
        {
            return null;
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
                using (var _db = ApplicationDbContext.Create())
                {
                    _db.DeleteRecipe(this.recipe.Id);
                    _db.SaveChanges();
                }

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
    }
}