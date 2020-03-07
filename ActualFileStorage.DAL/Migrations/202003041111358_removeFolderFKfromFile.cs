namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFolderFKfromFile : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Files", name: "FolderId", newName: "Folder_Id");
            RenameIndex(table: "dbo.Files", name: "IX_FolderId", newName: "IX_Folder_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Files", name: "IX_Folder_Id", newName: "IX_FolderId");
            RenameColumn(table: "dbo.Files", name: "Folder_Id", newName: "FolderId");
        }
    }
}
