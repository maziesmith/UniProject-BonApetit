namespace BonApetit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        AltText = c.String(),
                        Caption = c.String(),
                        ImageUrl = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Recipes", "Image_Id", c => c.Guid());
            CreateIndex("dbo.Recipes", "Image_Id");
            AddForeignKey("dbo.Recipes", "Image_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Image_Id", "dbo.Images");
            DropIndex("dbo.Recipes", new[] { "Image_Id" });
            DropColumn("dbo.Recipes", "Image_Id");
            DropTable("dbo.Images");
        }
    }
}
