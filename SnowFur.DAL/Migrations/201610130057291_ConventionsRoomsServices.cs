namespace SnowFur.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConventionsRoomsServices : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomMateOffers", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoomMateOffers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoomMateOffers", "User_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoomReservations", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.RoomMateOffers", new[] { "Id" });
            DropIndex("dbo.RoomMateOffers", new[] { "User_Id" });
            DropIndex("dbo.RoomMateOffers", new[] { "User_Id1" });
            DropIndex("dbo.RoomReservations", new[] { "Id" });
            DropPrimaryKey("dbo.RoomReservations");
            CreateTable(
                "dbo.ConventionPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        ConventionId = c.Int(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conventions", t => t.ConventionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ConventionId);
            
            CreateTable(
                "dbo.Conventions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nights = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConventionId = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                        Capacity = c.Int(nullable: false),
                        DateDeleted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conventions", t => t.ConventionId)
                .Index(t => t.ConventionId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConventionId = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 500),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        ChargeAfter = c.DateTime(),
                        DateDeleted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conventions", t => t.ConventionId)
                .Index(t => t.ConventionId);
            
            CreateTable(
                "dbo.ServiceOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ServiceId);
            
            AddColumn("dbo.PersonalProfiles", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.PersonalProfiles", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.PersonalProfiles", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoomReservations", "UserId", c => c.Int());
            AddColumn("dbo.RoomReservations", "RoomId", c => c.Int());
            AlterColumn("dbo.RoomReservations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RoomReservations", "Id");
            CreateIndex("dbo.RoomReservations", "UserId");
            CreateIndex("dbo.RoomReservations", "RoomId");
            AddForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms", "Id");
            AddForeignKey("dbo.RoomReservations", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.PersonalProfiles", "IsDeleted");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.RoomReservations", "Night1");
            DropColumn("dbo.RoomReservations", "Night2");
            DropColumn("dbo.RoomReservations", "Night3");
            DropColumn("dbo.RoomReservations", "AmountPayed");
            DropColumn("dbo.RoomReservations", "DatePaid");
            DropColumn("dbo.RoomReservations", "IsSponsor");
            DropColumn("dbo.RoomReservations", "IsDogAttending");
            DropColumn("dbo.RoomReservations", "IsVegetarian");
            DropTable("dbo.RoomMateOffers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomMateOffers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MessageForReceiver = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RoomReservations", "IsVegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "IsDogAttending", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "IsSponsor", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "DatePaid", c => c.DateTime());
            AddColumn("dbo.RoomReservations", "AmountPayed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RoomReservations", "Night3", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Night2", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomReservations", "Night1", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonalProfiles", "IsDeleted", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.RoomReservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceOrders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceOrders", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ConventionPayments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Services", "ConventionId", "dbo.Conventions");
            DropForeignKey("dbo.Rooms", "ConventionId", "dbo.Conventions");
            DropForeignKey("dbo.ConventionPayments", "ConventionId", "dbo.Conventions");
            DropIndex("dbo.ServiceOrders", new[] { "ServiceId" });
            DropIndex("dbo.ServiceOrders", new[] { "UserId" });
            DropIndex("dbo.RoomReservations", new[] { "RoomId" });
            DropIndex("dbo.RoomReservations", new[] { "UserId" });
            DropIndex("dbo.Services", new[] { "ConventionId" });
            DropIndex("dbo.Rooms", new[] { "ConventionId" });
            DropIndex("dbo.ConventionPayments", new[] { "ConventionId" });
            DropIndex("dbo.ConventionPayments", new[] { "UserId" });
            DropPrimaryKey("dbo.RoomReservations");
            AlterColumn("dbo.RoomReservations", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.RoomReservations", "RoomId");
            DropColumn("dbo.RoomReservations", "UserId");
            DropColumn("dbo.AspNetUsers", "DateCreated");
            DropColumn("dbo.AspNetUsers", "DateUpdated");
            DropColumn("dbo.AspNetUsers", "DateDeleted");
            DropColumn("dbo.PersonalProfiles", "DateCreated");
            DropColumn("dbo.PersonalProfiles", "DateUpdated");
            DropColumn("dbo.PersonalProfiles", "DateDeleted");
            DropTable("dbo.ServiceOrders");
            DropTable("dbo.Services");
            DropTable("dbo.Rooms");
            DropTable("dbo.Conventions");
            DropTable("dbo.ConventionPayments");
            AddPrimaryKey("dbo.RoomReservations", "Id");
            CreateIndex("dbo.RoomReservations", "Id");
            CreateIndex("dbo.RoomMateOffers", "User_Id1");
            CreateIndex("dbo.RoomMateOffers", "User_Id");
            CreateIndex("dbo.RoomMateOffers", "Id");
            AddForeignKey("dbo.RoomReservations", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RoomMateOffers", "User_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RoomMateOffers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RoomMateOffers", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
