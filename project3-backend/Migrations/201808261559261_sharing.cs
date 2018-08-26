namespace project3_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sharing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListSharings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        List_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.List_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListSharings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ListSharings", "List_Id", "dbo.Lists");
            DropIndex("dbo.ListSharings", new[] { "User_Id" });
            DropIndex("dbo.ListSharings", new[] { "List_Id" });
            DropTable("dbo.ListSharings");
        }
    }
}
