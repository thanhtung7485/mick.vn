using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.CodeFirst.TenantMusic
{
    public class MusicContext : DbContext, IMusicContext
    {   public MusicContext()
            : base("MusicContext")
        { }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Song>()
                .HasOptional(d => d.Author)
                .WithMany(e => e.Songs)
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<Song>()
                .HasOptional(d => d.Genre)
                .WithMany(e => e.Songs)
                .HasForeignKey(c => c.GenreId);
        }
    }
}

 