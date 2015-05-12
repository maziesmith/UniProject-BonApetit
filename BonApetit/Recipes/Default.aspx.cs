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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Recipe> GetRecipes()
        {
            var _db = new ApplicationDbContext();
            IQueryable<Recipe> query = _db.Recipes;
            return query;
        }
    }
}