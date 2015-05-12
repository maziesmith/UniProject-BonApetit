namespace BonApetit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesAndFavourites : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Categories", new[] { "Recipe_Id" });
            CreateTable(
                "dbo.RecipeCategories",
                c => new
                    {
                        Recipe_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_Id, t.Category_Id })
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Recipe_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.FavouriteRecipes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Recipe_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Recipe_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Recipe_Id);
            
            DropColumn("dbo.Categories", "Recipe_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Recipe_Id", c => c.Guid());
            DropForeignKey("dbo.FavouriteRecipes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.FavouriteRecipes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecipeCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.RecipeCategories", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.FavouriteRecipes", new[] { "Recipe_Id" });
            DropIndex("dbo.FavouriteRecipes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RecipeCategories", new[] { "Category_Id" });
            DropIndex("dbo.RecipeCategories", new[] { "Recipe_Id" });
            DropTable("dbo.FavouriteRecipes");
            DropTable("dbo.RecipeCategories");
            CreateIndex("dbo.Categories", "Recipe_Id");
            AddForeignKey("dbo.Categories", "Recipe_Id", "dbo.Recipes", "Id");
        }
    }
}
