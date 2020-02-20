namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passsalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salt", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt");
        }
    }
}
