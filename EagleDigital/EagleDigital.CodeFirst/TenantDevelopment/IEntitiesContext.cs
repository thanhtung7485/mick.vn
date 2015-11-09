using System.Data.Entity;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

namespace EagleDigital.CodeFirst.TenantDevelopment
{
    public interface IEntitiesContext
    {
      
        DbSet<AboutUs> AboutUs { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ContactUs> ContactUs { get; set; }
        DbSet<Domain> Domains { get; set; }
        DbSet<DomainInfor> DomainInfors { get; set; }
        DbSet<RequestProject> RequestProjects { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<TabName> TabNames { get; set; }

        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
       // DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
