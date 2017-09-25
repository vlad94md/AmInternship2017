namespace EnFlights.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CityFromId = c.Guid(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        CityToId = c.Guid(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        DurationInMinutes = c.Int(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSeats = c.Int(nullable: false),
                        BookedSeats = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityToId)
                .ForeignKey("dbo.Cities", t => t.CityFromId)
                .Index(t => t.CityFromId)
                .Index(t => t.CityToId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Title = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PassportNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PassportExpirationDate = c.DateTime(nullable: false),
                        Citizenship = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        FlightId = c.Guid(nullable: false),
                        BillingDetailsId = c.Guid(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        BaggageType = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FlightId);
            
            CreateTable(
                "dbo.BillingDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PassportNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PassportExpirationDate = c.DateTime(nullable: false),
                        Citizenship = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        TicketOrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketOrders", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FlightUsers",
                c => new
                    {
                        Flight_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Flight_Id, t.User_Id })
                .ForeignKey("dbo.Flights", t => t.Flight_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Flight_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "CityFromId", "dbo.Cities");
            DropForeignKey("dbo.Flights", "CityToId", "dbo.Cities");
            DropForeignKey("dbo.TicketOrders", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FlightUsers", "Flight_Id", "dbo.Flights");
            DropForeignKey("dbo.TicketOrders", "UserId", "dbo.Users");
            DropForeignKey("dbo.BillingDetails", "Id", "dbo.TicketOrders");
            DropIndex("dbo.FlightUsers", new[] { "User_Id" });
            DropIndex("dbo.FlightUsers", new[] { "Flight_Id" });
            DropIndex("dbo.BillingDetails", new[] { "Id" });
            DropIndex("dbo.TicketOrders", new[] { "FlightId" });
            DropIndex("dbo.TicketOrders", new[] { "UserId" });
            DropIndex("dbo.Flights", new[] { "CityToId" });
            DropIndex("dbo.Flights", new[] { "CityFromId" });
            DropTable("dbo.FlightUsers");
            DropTable("dbo.BillingDetails");
            DropTable("dbo.TicketOrders");
            DropTable("dbo.Users");
            DropTable("dbo.Flights");
            DropTable("dbo.Cities");
        }
    }
}
