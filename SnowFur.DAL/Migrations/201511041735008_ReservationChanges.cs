namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomReservations", "Night1", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Night2", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Night3", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "AmountPayed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RoomReservations", "IsVegetarian", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PersonalProfiles", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonalProfiles", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonalProfiles", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.PersonalProfiles", "ZipCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.PersonalProfiles", "City", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonalProfiles", "State", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "AdditionalInfo", c => c.String(maxLength: 500));
            AlterColumn("dbo.RoomReservations", "Note", c => c.String(maxLength: 500));
            DropColumn("dbo.RoomReservations", "Day1");
            DropColumn("dbo.RoomReservations", "Day2");
            DropColumn("dbo.RoomReservations", "Day3");
            DropColumn("dbo.RoomReservations", "Day4");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomReservations", "Day4", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Day3", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Day2", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Day1", c => c.Boolean(nullable: false));
            AlterColumn("dbo.RoomReservations", "Note", c => c.String());
            AlterColumn("dbo.AspNetUsers", "AdditionalInfo", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "State", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "City", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "ZipCode", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "Address", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "LastName", c => c.String());
            AlterColumn("dbo.PersonalProfiles", "FirstName", c => c.String());
            DropColumn("dbo.RoomReservations", "IsVegetarian");
            DropColumn("dbo.RoomReservations", "AmountPayed");
            DropColumn("dbo.RoomReservations", "Night3");
            DropColumn("dbo.RoomReservations", "Night2");
            DropColumn("dbo.RoomReservations", "Night1");
        }
    }
}
