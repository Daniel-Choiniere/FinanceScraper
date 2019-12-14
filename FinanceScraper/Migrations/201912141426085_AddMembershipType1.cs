namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "MerMemberShipType_Id", "dbo.MemberShipTypes");
            DropIndex("dbo.Users", new[] { "MerMemberShipType_Id" });
            DropColumn("dbo.Users", "MembershipTypeId");
            RenameColumn(table: "dbo.Users", name: "MerMemberShipType_Id", newName: "MembershipTypeId");
            DropPrimaryKey("dbo.MemberShipTypes");
            AddColumn("dbo.MemberShipTypes", "SignUpFee", c => c.Short(nullable: false));
            AlterColumn("dbo.Users", "MembershipTypeId", c => c.Byte(nullable: false));
            AlterColumn("dbo.MemberShipTypes", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.MemberShipTypes", "Id");
            CreateIndex("dbo.Users", "MembershipTypeId");
            AddForeignKey("dbo.Users", "MembershipTypeId", "dbo.MemberShipTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.MemberShipTypes", "SignUpFree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberShipTypes", "SignUpFree", c => c.Short(nullable: false));
            DropForeignKey("dbo.Users", "MembershipTypeId", "dbo.MemberShipTypes");
            DropIndex("dbo.Users", new[] { "MembershipTypeId" });
            DropPrimaryKey("dbo.MemberShipTypes");
            AlterColumn("dbo.MemberShipTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "MembershipTypeId", c => c.Int());
            DropColumn("dbo.MemberShipTypes", "SignUpFee");
            AddPrimaryKey("dbo.MemberShipTypes", "Id");
            RenameColumn(table: "dbo.Users", name: "MembershipTypeId", newName: "MerMemberShipType_Id");
            AddColumn("dbo.Users", "MembershipTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Users", "MerMemberShipType_Id");
            AddForeignKey("dbo.Users", "MerMemberShipType_Id", "dbo.MemberShipTypes", "Id");
        }
    }
}
