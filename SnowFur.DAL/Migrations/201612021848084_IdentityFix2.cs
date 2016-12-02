namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityFix2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RoomReservations");
            DropColumn("dbo.RoomReservations", "Idtmp");
            AddColumn("dbo.RoomReservations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RoomReservations", "Id");

        }

        public override void Down()
        {
            AddColumn("dbo.RoomReservations", "Idtmp", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.RoomReservations");
            DropColumn("dbo.RoomReservations", "Id");
            AddPrimaryKey("dbo.RoomReservations", "Idtmp");
        }
    }
}
