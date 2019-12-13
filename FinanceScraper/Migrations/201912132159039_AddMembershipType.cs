namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberShipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SignUpFree = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "MembershipTypeId", c => c.Byte(nullable: false));
            AddColumn("dbo.Users", "MerMemberShipType_Id", c => c.Int());
            CreateIndex("dbo.Users", "MerMemberShipType_Id");
            AddForeignKey("dbo.Users", "MerMemberShipType_Id", "dbo.MemberShipTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "MerMemberShipType_Id", "dbo.MemberShipTypes");
            DropIndex("dbo.Users", new[] { "MerMemberShipType_Id" });
            DropColumn("dbo.Users", "MerMemberShipType_Id");
            DropColumn("dbo.Users", "MembershipTypeId");
            DropTable("dbo.MemberShipTypes");
        }
    }
}
