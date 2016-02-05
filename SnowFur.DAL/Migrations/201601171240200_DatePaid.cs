namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatePaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomReservations", "DatePaid", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomReservations", "DatePaid");
        }
    }
}
