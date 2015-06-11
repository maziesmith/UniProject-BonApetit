namespace BonApetit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFavourites : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavouriteRecipes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavouriteRecipes", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.FavouriteRecipes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FavouriteRecipes", new[] { "Recipe_Id" });
            DropTable("dbo.FavouriteRecipes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FavouriteRecipes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Recipe_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Recipe_Id });

            CreateIndex("dbo.FavouriteRecipes", "Recipe_Id");
            CreateIndex("dbo.FavouriteRecipes", "ApplicationUser_Id");
            AddForeignKey("dbo.FavouriteRecipes", "Recipe_Id", "dbo.Recipes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FavouriteRecipes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
