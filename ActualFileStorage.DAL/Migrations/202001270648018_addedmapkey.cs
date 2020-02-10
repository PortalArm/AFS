namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmapkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Id", "dbo.Folders");
            DropIndex("dbo.Users", new[] { "Id" });
            AddColumn("dbo.Users", "FolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "FolderId");
            AddForeignKey("dbo.Users", "FolderId", "dbo.Folders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "FolderId", "dbo.Folders");
            DropIndex("dbo.Users", new[] { "FolderId" });
            DropColumn("dbo.Users", "FolderId");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Id", "dbo.Folders", "Id");
        }
    }
}
