using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BonApetit.Controls.Forms;
using BonApetit.Models;

namespace BonApetit.Recipes
{
    public partial class ManageRecipe : System.Web.UI.Page
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Recipe recipe = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                this.ErrorMessage.Visible = !string.IsNullOrWhiteSpace(this.FailureText.Text);
        }

        protected void SaveRecipe(object sender, EventArgs e)
        {
            BonApetit.Models.Image image = null;

            if (ImageUpload.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(ImageUpload.FileName).ToLower();
                string[] supportedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                bool isFileExtensionSupported = supportedExtensions.Contains(fileExtension);

                if (isFileExtensionSupported)
                {
                    string imagesPath = Server.MapPath("~/Recipes/Images/");
                    var physicalImageUrl = imagesPath + ImageUpload.FileName;

                    // Save to Images folder.
                    ImageUpload.PostedFile.SaveAs(physicalImageUrl);

                    var imageTitle = System.IO.Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    var imageUrl = System.IO.Path.GetFileName(ImageUpload.FileName);
                    image = new BonApetit.Models.Image()
                    {
                        Title = imageTitle,
                        AltText = imageTitle,
                        Caption = imageTitle,
                        CreatedDate = DateTime.Now,
                        ImageUrl = imageUrl
                    };
                }
                else
                {
                    FailureText.Text = "Unsupported file type.";
                    return;
                }
            }

            Guid recipeId;
            if (Guid.TryParse(Request.QueryString["recipeId"], out recipeId))
            {
                try
                {
                    this.recipe = db.Recipes.Find(recipeId);

                    //IEnumerable<string> ingredientValues = this.Ingredients.GetAllValues();
                    //var newIngredients = ingredientValues.Where(newi => this.recipe.Ingredients.Any(i => i.Content != newi)).Select(i => (Ingredient)i);
                    //var ingredients = this.recipe.Ingredients.Where(i => ingredientValues.Contains(i.Content)).ToList();
                    //ingredients.AddRange(newIngredients);

                    //var removedIngredients = this.recipe.Ingredients.Where(i => !ingredientValues.Contains(i.Content));

                    this.recipe.Name = this.Name.Text;
                    this.recipe.Description = HttpUtility.HtmlDecode(this.Description.Text);
                    this.recipe.PrepareInstructions = HttpUtility.HtmlDecode(this.PreparationInstructions.Text);
                    this.recipe.Image = image ?? this.recipe.Image;

                    this.EditIngredients();
                    this.EditCategories();

                    db.Entry(recipe).State = EntityState.Modified;
                    db.SaveChanges();

                    Response.Redirect("~/Recipes/RecipeDetails?recipeId=" + recipe.Id);
                }
                catch (Exception ex)
                {
                    FailureText.Text = ex.Message;
                }
            }
        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public BonApetit.Models.Recipe EditForm_GetItem()
        {
            Guid recipeId;
            if (Guid.TryParse(Request.QueryString["recipeId"], out recipeId))
            {
                this.recipe = db.Recipes.Find(recipeId);
            }

            return this.recipe;
        }

        protected void Ingredients_DataBinding(object sender, EventArgs args)
        {
            var ingredients = sender as DynamicTextBox;
            ingredients.InitializeControl(this.recipe.Ingredients.Select(i => i.Content));
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = db.GetCategories();
            return categories;
        }

        protected void CategoriesList_DataBound(object sender, EventArgs e)
        {
            foreach (var category in this.recipe.Categories)
            {
                var listItem = this.CategoriesList.Items.FindByValue(category.Id.ToString());
                listItem.Selected = true;
            }
        }

        protected void NewCategoryButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.NewCategory.Text))
            {
                try
                {
                    var category = new Category(this.NewCategory.Text);
                    db.AddCategory(category);
                    db.SaveChanges();

                    var listItem = new ListItem(category.Name, category.Id.ToString());
                    listItem.Selected = true;
                    this.CategoriesList.Items.Add(listItem);
                }
                catch (Exception ex)
                {

                }
            }

            this.NewCategory.Text = string.Empty;
        }

        #region Elements

        private TableCell EditFormContent
        {
            get
            {
                return this.EditForm.Row.Cells[0];
            }
        }

        private TextBox Name
        {
            get
            {
                return this.EditForm.FindControl("Name") as TextBox;
            }
        }

        private TextBox Description
        {
            get
            {
                return this.EditForm.FindControl("Description") as TextBox;
            }
        }

        private TextBox PreparationInstructions
        {
            get
            {
                return this.EditForm.FindControl("PreparationInstructions") as TextBox;
            }
        }

        private DynamicTextBox Ingredients
        {
            get
            {
                return this.EditForm.FindControl("Ingredients") as DynamicTextBox;
            }
        }

        private ListBox CategoriesList
        {
            get
            {
                return this.EditForm.FindControl("CategoriesList") as ListBox;
            }
        }

        private TextBox NewCategory
        {
            get
            {
                return this.EditForm.FindControl("NewCategory") as TextBox;
            }
        }

        private FileUpload ImageUpload
        {
            get
            {
                return this.EditForm.FindControl("ImageUpload") as FileUpload;
            }
        }

        #endregion

        private void EditIngredients()
        {
            var newIngredients = new List<Ingredient>();

            foreach (var ingredientContent in this.Ingredients.GetAllValues())
            {
                Ingredient ingredient;
                var existingIngredient = recipe.Ingredients.FirstOrDefault(i => i.Content == ingredientContent);
                if (existingIngredient == null)
                    ingredient = new Ingredient() { Content = ingredientContent };
                else
                {
                    ingredient = existingIngredient;
                    this.recipe.Ingredients.Remove(existingIngredient);
                }

                newIngredients.Add(ingredient);
            }

            db.Delete(this.recipe.Ingredients);

            this.recipe.Ingredients = newIngredients;
        }

        private void EditCategories()
        {
            foreach (var index in this.CategoriesList.GetSelectedIndices())
            {
                var categoryId = new Guid(this.CategoriesList.Items[index].Value);
                var existing = this.recipe.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (existing == null)
                    this.recipe.Categories.Add(db.GetCategory(categoryId));
            }

            var categoriesToRemove = recipe.Categories.Where(c => !this.CategoriesList.Items.FindByValue(c.Id.ToString()).Selected).ToList();
            foreach (var categoryToRemove in categoriesToRemove)
                this.recipe.Categories.Remove(categoryToRemove);
        }
    }
}