namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(maxLength: 70),
                        City = c.String(maxLength: 40),
                        State = c.String(maxLength: 40),
                        Country = c.String(maxLength: 40),
                        PostalCode = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 24),
                        Fax = c.String(maxLength: 24),
                        Email = c.String(nullable: false, maxLength: 60),
                        StatusId = c.String(nullable: false, maxLength: 60),
                        InsAt = c.DateTime(nullable: false),
                        InsBy = c.String(nullable: false, maxLength: 50),
                        UpdAt = c.DateTime(nullable: false),
                        UpdBy = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        BillingAddress = c.String(maxLength: 70),
                        BillingCity = c.String(maxLength: 40),
                        BillingState = c.String(maxLength: 40),
                        BillingCountry = c.String(maxLength: 40),
                        BillingPostalCode = c.String(maxLength: 10),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatusId = c.String(nullable: false, maxLength: 60),
                        InsAt = c.DateTime(nullable: false),
                        InsBy = c.String(nullable: false, maxLength: 50),
                        UpdAt = c.DateTime(nullable: false),
                        UpdBy = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Company = c.String(maxLength: 80),
                        SupportRepId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customer", new[] { "PersonId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropForeignKey("dbo.Customer", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropTable("dbo.Customer");
            DropTable("dbo.Invoice");
            DropTable("dbo.Person");
        }
    }
}
