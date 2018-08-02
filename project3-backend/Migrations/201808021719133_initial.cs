namespace project3_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Owner_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.ListItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        List_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Subject = c.String(),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.ListItems", "List_Id", "dbo.Lists");
            DropIndex("dbo.ListItems", new[] { "List_Id" });
            DropIndex("dbo.Lists", new[] { "Owner_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.ListItems");
            DropTable("dbo.Lists");
        }
    }
}
