﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.DbFirst.Model;

namespace EagleDigital.DbFirst
{
    public interface IEntitiesContext : IDisposable
    {
        DbSet<AboutU> AboutUs { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<ContactU> ContactUs { get; set; }
        DbSet<Domain> Domains { get; set; }
        DbSet<DomainInfor> DomainInfors { get; set; }
        DbSet<RequestProject> RequestProjects { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<TabName> TabNames { get; set; }


         int SaveChanges();
         DbSet<T> Set<T>() where T : class;
         DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
