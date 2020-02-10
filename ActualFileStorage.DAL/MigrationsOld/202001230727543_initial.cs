namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Hash = c.String(nullable: false),
                        Size = c.Long(nullable: false),
                        Ext = c.String(),
                        FolderId = c.Int(nullable: false),
                        Visibility = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.FolderId, cascadeDelete: true)
                .Index(t => t.FolderId);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Visibility = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 32),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        SecondName = c.String(),
                        BirthDate = c.DateTime(),
                        FolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Folders", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Login, unique: true);
            
            CreateTable(
                "dbo.LoginInfoes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PassHash = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FolderId", "dbo.Folders");
            DropForeignKey("dbo.LoginInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Id", "dbo.Folders");
            DropIndex("dbo.LoginInfoes", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Login" });
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.Files", new[] { "FolderId" });
            DropTable("dbo.LoginInfoes");
            DropTable("dbo.Users");
            DropTable("dbo.Folders");
            DropTable("dbo.Files");
        }
    }
}
