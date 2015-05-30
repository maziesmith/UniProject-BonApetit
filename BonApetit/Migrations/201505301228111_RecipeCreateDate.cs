namespace BonApetit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "CreateDate");
        }
    }
}
