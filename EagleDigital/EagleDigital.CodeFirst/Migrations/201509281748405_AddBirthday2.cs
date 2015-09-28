namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthday2 : DbMigration
    {
        public override void Up()
        {
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
            
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "PersonId");
            CreateIndex("dbo.Customer", "SupportRepId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customer", new[] { "SupportRepId" });
            DropIndex("dbo.Employee", new[] { "ReportsTo" });
            DropIndex("dbo.Employee", new[] { "PersonId" });
            DropForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee");
            DropForeignKey("dbo.Employee", "PersonId", "dbo.Person");
            DropTable("dbo.Employee");
        }
    }
}
