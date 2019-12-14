namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameFieldToMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberShipTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberShipTypes", "Name");
        }
    }
}
