using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Models;

namespace BonApetit.Recipes
{
    public partial class Recipes : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Recipe> GetRecipes()
        {
            int pageId;
            int.TryParse(Request.QueryString["id"], out pageId);

            string category = Request.QueryString["category"];

            var allRecipes = db.GetRecipes(category).OrderByDescending(r => r.CreateDate);

            var totalRecipesCount = allRecipes.Count();
            var recipes = allRecipes.Skip(pageId * 4).Take(4);

            return recipes;
        }

        public IEnumerable<Recipe> LatestRecipes_GetData()
        {
            var allRecipes = db.GetRecipes().OrderByDescending(r => r.CreateDate);
            var latestRecipes = allRecipes.Take(3);
            return latestRecipes;
        }

        public IEnumerable<Category> Categories_GetData()
        {
            var categories = db.GetCategories();
            return categories;
        }
    }
}