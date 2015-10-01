namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
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
                        Name = c.String(nullable: false, maxLength: 100),
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
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.TabName", t => t.TabNameId, cascadeDelete: true)
                .Index(t => t.DomainId)
                .Index(t => t.TabNameId);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.RequestProject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(),
                        Title = c.String(maxLength: 250),
                        Content = c.String(),
                        Contact = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domain", t => t.DomainId)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AboutUs", "TabNameId", "dbo.TabName");
            DropForeignKey("dbo.DomainInfor", "TabNameId", "dbo.TabName");
            DropForeignKey("dbo.DomainInfor", "DomainId", "dbo.Domain");
            DropForeignKey("dbo.Domain", "SubCategoryId", "dbo.SubCategory");
            DropForeignKey("dbo.SubCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.RequestProject", "DomainId", "dbo.Domain");
            DropIndex("dbo.SubCategory", new[] { "CategoryId" });
            DropIndex("dbo.RequestProject", new[] { "DomainId" });
            DropIndex("dbo.Domain", new[] { "SubCategoryId" });
            DropIndex("dbo.DomainInfor", new[] { "TabNameId" });
            DropIndex("dbo.DomainInfor", new[] { "DomainId" });
            DropIndex("dbo.AboutUs", new[] { "TabNameId" });
            DropTable("dbo.ContactUs");
            DropTable("dbo.Category");
            DropTable("dbo.SubCategory");
            DropTable("dbo.RequestProject");
            DropTable("dbo.Domain");
            DropTable("dbo.DomainInfor");
            DropTable("dbo.TabName");
            DropTable("dbo.AboutUs");
        }
    }
}
