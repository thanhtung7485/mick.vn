namespace EagleDigital.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerequried : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TabName", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Domain", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Domain", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.SubCategory", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.SubCategory", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.Category", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Category", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.RequestProject", "Title", c => c.String(maxLength: 250));
            AlterColumn("dbo.RequestProject", "Content", c => c.String());
            AlterColumn("dbo.RequestProject", "Contact", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RequestProject", "Contact", c => c.String(nullable: false));
            AlterColumn("dbo.RequestProject", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.RequestProject", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Category", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SubCategory", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.SubCategory", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Domain", "Description", c => c.String());
            AlterColumn("dbo.Domain", "Name", c => c.String());
            AlterColumn("dbo.TabName", "Name", c => c.String(nullable: false));
        }
    }
}
