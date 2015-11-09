namespace EagleDigital.CodeFirst.TenantMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Song", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Song", "GenreId", "dbo.Genre");
            DropIndex("dbo.Song", new[] { "GenreId" });
            DropIndex("dbo.Song", new[] { "AuthorId" });
            AlterColumn("dbo.Song", "GenreId", c => c.Int());
            AlterColumn("dbo.Song", "AuthorId", c => c.Int());
            CreateIndex("dbo.Song", "GenreId");
            CreateIndex("dbo.Song", "AuthorId");
            AddForeignKey("dbo.Song", "AuthorId", "dbo.Author", "Id");
            AddForeignKey("dbo.Song", "GenreId", "dbo.Genre", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Song", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Song", "AuthorId", "dbo.Author");
            DropIndex("dbo.Song", new[] { "AuthorId" });
            DropIndex("dbo.Song", new[] { "GenreId" });
            AlterColumn("dbo.Song", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Song", "GenreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Song", "AuthorId");
            CreateIndex("dbo.Song", "GenreId");
            AddForeignKey("dbo.Song", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Song", "AuthorId", "dbo.Author", "Id", cascadeDelete: true);
        }
    }
}
