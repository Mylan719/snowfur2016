namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sponsorship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomReservations", "IsSponsor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomReservations", "IsSponsor");
        }
    }
}
