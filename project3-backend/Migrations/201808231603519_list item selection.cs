namespace project3_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listitemselection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListItems", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListItems", "IsSelected");
        }
    }
}
