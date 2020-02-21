namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shortlinks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ShortLink", c => c.String());
            AddColumn("dbo.Folders", "ShortLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Folders", "ShortLink");
            DropColumn("dbo.Files", "ShortLink");
        }
    }
}
