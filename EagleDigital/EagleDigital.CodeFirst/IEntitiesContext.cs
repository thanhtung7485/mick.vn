using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.Models;

namespace EagleDigital.CodeFirst
{
    public interface IEntitiesContext
    {
      
        IDbSet<Person> People { get; set; }
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Customer> Customers { get; set; }
       
        IDbSet<Invoice> Invoices { get; set; }
     

        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
    }
}
