namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConventionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conventions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Conventions", "Description", c => c.String());
            AddColumn("dbo.Conventions", "Activities", c => c.String());
            AddColumn("dbo.Conventions", "ContactInfo", c => c.String());
            AddColumn("dbo.Conventions", "PlaceName", c => c.String());
            AddColumn("dbo.Conventions", "GpsLatitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Conventions", "GpsLongitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conventions", "GpsLongitude");
            DropColumn("dbo.Conventions", "GpsLatitude");
            DropColumn("dbo.Conventions", "PlaceName");
            DropColumn("dbo.Conventions", "ContactInfo");
            DropColumn("dbo.Conventions", "Activities");
            DropColumn("dbo.Conventions", "Description");
            DropColumn("dbo.Conventions", "IsActive");
        }
    }
}
