namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityFix1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RoomReservations");
            AddColumn("dbo.RoomReservations", "Idtmp", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RoomReservations", "Idtmp");
            DropColumn("dbo.RoomReservations", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomReservations", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.RoomReservations");
            DropColumn("dbo.RoomReservations", "Idtmp");
            AddPrimaryKey("dbo.RoomReservations", "Id");
        }
    }
}
