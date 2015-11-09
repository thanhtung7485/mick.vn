namespace EagleDigital.CodeFirst.TenantDevelopment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TabName", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TabName", "Description", c => c.String());
        }
    }
}
