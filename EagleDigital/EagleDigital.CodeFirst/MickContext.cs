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

  
        public IDbSet<Person> People { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Invoice> Invoices { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();




            modelBuilder.Entity<Customer>()
                .HasOptional(c => c.SupportRep)
                .WithMany(e => e.Customers)
                .HasForeignKey(c => c.SupportRepId);

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.Superior)
                .WithMany(s => s.Subordinates)
                .HasForeignKey(c => c.ReportsTo);

            modelBuilder.Entity<Invoice>()
                .HasRequired(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);  
        }

    }
}
