namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
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
                "dbo.AboutUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TabNameId = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TabName", t => t.TabNameId, cascadeDelete: true)
                .Index(t => t.TabNameId);
            
            CreateTable(
                "dbo.TabName",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DomainInfor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(nullable: false),
                        TabNameId = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TabName", t => t.TabNameId, cascadeDelete: true)
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .Index(t => t.TabNameId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestProject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domain", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.Employee", t => t.ReportsTo)
                .Index(t => t.PersonId)
                .Index(t => t.ReportsTo);
            
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
                .ForeignKey("dbo.Employee", t => t.SupportRepId)
                .Index(t => t.PersonId)
                .Index(t => t.SupportRepId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customer", new[] { "SupportRepId" });
            DropIndex("dbo.Customer", new[] { "PersonId" });
            DropIndex("dbo.Employee", new[] { "ReportsTo" });
            DropIndex("dbo.Employee", new[] { "PersonId" });
            DropIndex("dbo.RequestProject", new[] { "DomainId" });
            DropIndex("dbo.SubCategory", new[] { "CategoryId" });
            DropIndex("dbo.Domain", new[] { "SubCategoryId" });
            DropIndex("dbo.DomainInfor", new[] { "DomainId" });
            DropIndex("dbo.DomainInfor", new[] { "TabNameId" });
            DropIndex("dbo.AboutUs", new[] { "TabNameId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee");
            DropForeignKey("dbo.Customer", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee");
            DropForeignKey("dbo.Employee", "PersonId", "dbo.Person");
            DropForeignKey("dbo.RequestProject", "DomainId", "dbo.Domain");
            DropForeignKey("dbo.SubCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Domain", "SubCategoryId", "dbo.SubCategory");
            DropForeignKey("dbo.DomainInfor", "DomainId", "dbo.Domain");
            DropForeignKey("dbo.DomainInfor", "TabNameId", "dbo.TabName");
            DropForeignKey("dbo.AboutUs", "TabNameId", "dbo.TabName");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropTable("dbo.Customer");
            DropTable("dbo.Employee");
            DropTable("dbo.ContactUs");
            DropTable("dbo.RequestProject");
            DropTable("dbo.Category");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Domain");
            DropTable("dbo.DomainInfor");
            DropTable("dbo.TabName");
            DropTable("dbo.AboutUs");
            DropTable("dbo.Invoice");
            DropTable("dbo.Person");
        }
    }
}
