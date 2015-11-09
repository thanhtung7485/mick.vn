using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.Service.TenantMusic.IServices
{
    public interface IAuthorService
    {
        IQueryable<Author> List();
        Author Details(int id);
        Author Insert(Author author);
        Author Update(Author author);
    }
}
