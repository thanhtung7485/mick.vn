namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AboutUs", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AboutUs", "Content");
        }
    }
}
