namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterconfig : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Id" });
            DropColumn("dbo.Users", "FolderId");
            RenameColumn(table: "dbo.Users", name: "Id", newName: "FolderId");
            AlterColumn("dbo.Users", "FolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "FolderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "FolderId" });
            AlterColumn("dbo.Users", "FolderId", c => c.Int(nullable: false, identity: true));
            RenameColumn(table: "dbo.Users", name: "FolderId", newName: "Id");
            AddColumn("dbo.Users", "FolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Id");
        }
    }
}
