namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailsToStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "FullName", c => c.String());
            AddColumn("dbo.Stocks", "DatePublic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "DatePublic");
            DropColumn("dbo.Stocks", "FullName");
        }
    }
}
