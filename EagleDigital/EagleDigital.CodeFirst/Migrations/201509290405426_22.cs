namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Employee", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee");
            DropForeignKey("dbo.Customer", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee");
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropIndex("dbo.Employee", new[] { "PersonId" });
            DropIndex("dbo.Employee", new[] { "ReportsTo" });
            DropIndex("dbo.Customer", new[] { "PersonId" });
            DropIndex("dbo.Customer", new[] { "SupportRepId" });
            DropTable("dbo.Person");
            DropTable("dbo.Invoice");
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Company = c.String(maxLength: 80),
                        SupportRepId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Title = c.String(maxLength: 30),
                        BirthDate = c.DateTime(),
                        HireDate = c.DateTime(),
                        ReportsTo = c.Int(),
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
                .PrimaryKey(t => t.InvoiceId);
            
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
            
            CreateIndex("dbo.Customer", "SupportRepId");
            CreateIndex("dbo.Customer", "PersonId");
            CreateIndex("dbo.Employee", "ReportsTo");
            CreateIndex("dbo.Employee", "PersonId");
            CreateIndex("dbo.Invoice", "CustomerId");
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "PersonId");
            AddForeignKey("dbo.Customer", "PersonId", "dbo.Person", "PersonId");
            AddForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee", "PersonId");
            AddForeignKey("dbo.Employee", "PersonId", "dbo.Person", "PersonId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "PersonId", cascadeDelete: true);
        }
    }
}
