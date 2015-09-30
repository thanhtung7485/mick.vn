using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.Models;

namespace EagleDigital.CodeFirst
{
    public class MickContext : DbContext, IEntitiesContext
    {
        public MickContext()
            : base("MickContext")
        { }


        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<DomainInfor> DomainInfors { get; set; }
        public DbSet<RequestProject> RequestProjects { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TabName> TabNames { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<AboutUs>()
                .HasRequired(c => c.TabName)
                .WithMany(e => e.AboutUs)
                .HasForeignKey(c => c.TabNameId);

            modelBuilder.Entity<ContactUs>();

            modelBuilder.Entity<SubCategory>()
                .HasRequired(d => d.Category)
                .WithMany(e => e.SubCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Domain>()
                .HasRequired(d => d.SubCategory)
                .WithMany(e => e.Domains)
                .HasForeignKey(c => c.SubCategoryId);

            modelBuilder.Entity<DomainInfor>()
                 .HasRequired(d => d.Domain)
                 .WithMany(e => e.DomainInfors)
                 .HasForeignKey(c => c.DomainId);

            modelBuilder.Entity<RequestProject>()
                 .HasOptional(d => d.Domain)
                 .WithMany(e => e.RequestProjects)
                 .HasForeignKey(c => c.DomainId);


            

        }
    }
}
