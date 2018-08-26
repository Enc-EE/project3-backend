namespace project3_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListItemGroups",
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
            
            AddColumn("dbo.ListItems", "ListItemGroup_Id", c => c.Long());
            AddColumn("dbo.Lists", "IsGroupingEnabled", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ListItems", "ListItemGroup_Id");
            AddForeignKey("dbo.ListItems", "ListItemGroup_Id", "dbo.ListItemGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListItems", "ListItemGroup_Id", "dbo.ListItemGroups");
            DropForeignKey("dbo.ListItemGroups", "List_Id", "dbo.Lists");
            DropIndex("dbo.ListItems", new[] { "ListItemGroup_Id" });
            DropIndex("dbo.ListItemGroups", new[] { "List_Id" });
            DropColumn("dbo.Lists", "IsGroupingEnabled");
            DropColumn("dbo.ListItems", "ListItemGroup_Id");
            DropTable("dbo.ListItemGroups");
        }
    }
}
