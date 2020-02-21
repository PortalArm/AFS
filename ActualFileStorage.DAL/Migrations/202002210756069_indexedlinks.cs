namespace ActualFileStorage.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indexedlinks : DbMigration
    {
        private string indexName = "IX_Link";
        public override void Up()
        {
            AlterColumn("dbo.Files", "ShortLink", c => c.String(maxLength: 64));
            AlterColumn("dbo.Folders", "ShortLink", c => c.String(maxLength: 64));

            Sql(string.Format(@"
    CREATE UNIQUE NONCLUSTERED INDEX {0}
    ON {1}({2}) 
    WHERE {2} IS NOT NULL;",
    indexName, "dbo.Files", "ShortLink"));

            //CreateIndex("dbo.Files", "ShortLink", unique: true);
            //CreateIndex("dbo.Folders", "ShortLink", unique: true);

            Sql(string.Format(@"
    CREATE UNIQUE NONCLUSTERED INDEX {0}
    ON {1}({2}) 
    WHERE {2} IS NOT NULL;",
    indexName, "dbo.Folders", "ShortLink"));

        }
        
        public override void Down()
        {
            DropIndex("dbo.Folders", indexName);
            DropIndex("dbo.Files", indexName);
            AlterColumn("dbo.Folders", "ShortLink", c => c.String());
            AlterColumn("dbo.Files", "ShortLink", c => c.String());
        }
    }
}
