using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.Service.TenantMusic.IServices
{
    public interface IGenreService
    {
        IQueryable<Genre> List();
        Genre Details(int id);
        Genre Insert(Genre genre);
        Genre Update(Genre genre);
    }
}
