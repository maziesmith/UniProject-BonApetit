using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Models;

namespace BonApetit.Recipes
{
    public partial class AddRecipe : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ErrorMessage.Visible = !string.IsNullOrWhiteSpace(this.FailureText.Text);
        }

        protected void SaveRecipe(object sender, EventArgs e)
        {
            bool isFileExtensionSupported = false;
            string imagesPath = Server.MapPath("~/Recipes/Images/");
            if (ImageUpload.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(ImageUpload.FileName).ToLower();
                string[] supportedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                isFileExtensionSupported = supportedExtensions.Contains(fileExtension);
            }

            if (isFileExtensionSupported)
            {
                try
                {
                    var physicalImageUrl = imagesPath + ImageUpload.FileName;

                    // Save to Images folder.
                    ImageUpload.PostedFile.SaveAs(physicalImageUrl);
                    // Save to Images/Thumbs folder.
                    //ImageUpload.PostedFile.SaveAs(imagesPath + "Thumbs/" + ImageUpload.FileName);

                    var imageTitle = System.IO.Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    var imageUrl = System.IO.Path.GetFileName(ImageUpload.FileName);
                    var image = new BonApetit.Models.Image()
                    {
                        Title = imageTitle,
                        AltText = imageTitle,
                        Caption = imageTitle,
                        CreatedDate = DateTime.Now,
                        ImageUrl = imageUrl
                    };

                    List<Ingredient> ingredients = this.Ingredients.GetAllValues().Select(s => (Ingredient)s).ToList();

                    var recipe = new Recipe(this.Name.Text)
                    {
                        Description = this.Description.Text,
                        PrepareInstructions = this.PreparationInstructions.Text,
                        Image = image,
                        Ingredients = ingredients,
                    };

                    db.AddRecipe(recipe);
                    db.SaveChanges();

                    Response.Redirect("~/Recipes/RecipeDetails?recipeId=" + recipe.Id);
                }
                catch (Exception ex)
                {
                    FailureText.Text = ex.Message;
                }
            }
            else
            {
                FailureText.Text = "Unsupported file type.";
            }
        }
    }
}