namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AboutUs", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AboutUs", "Content", c => c.String());
        }
    }
}
