namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMemebrshipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MemberShipTypes (SignUpFree, DurationInMonths, DiscountRate) VALUES (0, 0, 0)");
            Sql("INSERT INTO MemberShipTypes (SignUpFree, DurationInMonths, DiscountRate) VALUES (30, 1, 10)");
            Sql("INSERT INTO MemberShipTypes (SignUpFree, DurationInMonths, DiscountRate) VALUES (90, 3, 15)");
            Sql("INSERT INTO MemberShipTypes (SignUpFree, DurationInMonths, DiscountRate) VALUES (300, 12, 20)");
        }

        public override void Down()
        {
        }
    }
}
