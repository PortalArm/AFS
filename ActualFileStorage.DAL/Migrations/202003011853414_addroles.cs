namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addroles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        WebRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.WebRole_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.WebRoles", t => t.WebRole_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.WebRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "WebRole_Id", "dbo.WebRoles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "WebRole_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.WebRoles");
        }
    }
}
