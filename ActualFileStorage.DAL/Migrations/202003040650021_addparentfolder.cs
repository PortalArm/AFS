namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addparentfolder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Folders", "ParentFolder_Id", c => c.Int());
            CreateIndex("dbo.Folders", "ParentFolder_Id");
            AddForeignKey("dbo.Folders", "ParentFolder_Id", "dbo.Folders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Folders", "ParentFolder_Id", "dbo.Folders");
            DropIndex("dbo.Folders", new[] { "ParentFolder_Id" });
            DropColumn("dbo.Folders", "ParentFolder_Id");
        }
    }
}
