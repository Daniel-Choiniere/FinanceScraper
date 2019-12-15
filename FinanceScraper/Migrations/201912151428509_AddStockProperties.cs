namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        LastPrice = c.String(),
                        Change = c.String(),
                        Currency = c.String(),
                        DataCollectedOn = c.String(),
                        Volume = c.String(),
                        AvgVol3m = c.String(),
                        MarketCap = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stocks");
        }
    }
}
