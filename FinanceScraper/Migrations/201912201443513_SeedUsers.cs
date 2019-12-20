namespace FinanceScraper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9852a988-383b-45ec-aace-246d92fce149', N'admin@yahoo.com', 0, N'ANixQrmTbXBhcXyoEv117tIBdK6rvt3g8/Ti3WpfK7N2X/cKUqoMWjWvTtRiYhOEDA==', N'f0b73262-3028-4947-a0e8-e2787c56943d', NULL, 0, 0, NULL, 1, 0, N'admin@yahoo.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ab4d8d68-ee94-41b5-b65a-27d81837b430', N'guest@yahoo.com', 0, N'AMP+B8c7Cbgj72QSAGs0RWsQkwPMVxpFomV4KUxDl0OhF+1Wk+eFO/1mMvjE9zm4qw==', N'23afca7e-d0f2-48b3-aa1c-08303cd00624', NULL, 0, 0, NULL, 1, 0, N'guest@yahoo.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e58260d7-2dd0-427e-9384-40861da5c351', N'CanManageStocks')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9852a988-383b-45ec-aace-246d92fce149', N'e58260d7-2dd0-427e-9384-40861da5c351')
");
        }
        
        public override void Down()
        {
        }
    }
}
