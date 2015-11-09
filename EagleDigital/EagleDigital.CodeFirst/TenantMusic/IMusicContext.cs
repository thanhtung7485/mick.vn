using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.CodeFirst.TenantMusic
{
    public interface IMusicContext
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Song> Songs { get; set; }
     
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
        // DbEntityEntry<T> Entry<T>(T entity) where T : class;

    }
}
