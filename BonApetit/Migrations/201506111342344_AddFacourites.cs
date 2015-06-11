namespace BonApetit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFacourites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserRecipes",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRecipes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.ApplicationUserRecipes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserRecipes", new[] { "Recipe_Id" });
            DropIndex("dbo.ApplicationUserRecipes", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserRecipes");
        }
    }
}
