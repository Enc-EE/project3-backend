namespace project3_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listnamenotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lists", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lists", "Name", c => c.String());
        }
    }
}
