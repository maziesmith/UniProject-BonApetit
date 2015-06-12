using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Models;
using Utils.Web;

namespace BonApetit.Recipes
{
    public partial class Recipes : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private bool favouritesOnly = false;

        private const string FavouritesQuery = "favouritesOnly";
        private const string CategoriesQuery = "category";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Recipe> GetRecipes()
        {
            int pageId;
            int.TryParse(Request.QueryString["id"], out pageId);
            string category = Request.QueryString[CategoriesQuery];

            var allRecipes = db.GetRecipes(category, favouritesOnly);
            var totalRecipesCount = allRecipes.Count();
            var recipes = allRecipes.OrderByDescending(r => r.CreateDate).Skip(pageId * 4).Take(4);

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

        protected void CategoryLink_PreRender(object sender, EventArgs e)
        {
            var link = sender as HyperLink;
            var query = QueryString.Current.Add(CategoriesQuery, link.Text, true);
            link.NavigateUrl = ResolveUrl("~/Recipes/Default.aspx") + query.ToString();
        }

        protected void FavouritesButton_Load(object sender, EventArgs e)
        {
            bool.TryParse(Request.QueryString[FavouritesQuery], out this.favouritesOnly);

            QueryString query;
            if (this.favouritesOnly)
            {
                query = QueryString.Current.Remove(FavouritesQuery);
                this.FavouritesButton.Text = "Show all";
            }
            else
            {
                query = QueryString.Current.Add(FavouritesQuery, "true", true);
                this.FavouritesButton.Text = "Show favourites";
            }

            this.FavouritesButton.NavigateUrl = ResolveUrl("~/Recipes/Default.aspx") + query.ToString();
        }
    }
}