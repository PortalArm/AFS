namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class troubleshooting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoginInfoes", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "FolderId" });
            DropColumn("dbo.Users", "Id");
            RenameColumn(table: "dbo.Users", name: "FolderId", newName: "Id");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "FolderId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.LoginInfoes", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginInfoes", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Id" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "FolderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "Id");
            RenameColumn(table: "dbo.Users", name: "Id", newName: "FolderId");
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Users", "FolderId");
            AddForeignKey("dbo.LoginInfoes", "UserId", "dbo.Users", "Id");
        }
    }
}
