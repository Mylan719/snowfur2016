namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoomReservations", "IsPayed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomReservations", "IsPayed", c => c.Boolean(nullable: false));
        }
    }
}
