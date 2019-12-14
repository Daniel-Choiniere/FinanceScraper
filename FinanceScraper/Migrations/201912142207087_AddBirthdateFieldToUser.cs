namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthdateFieldToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Birthdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Birthdate");
        }
    }
}
