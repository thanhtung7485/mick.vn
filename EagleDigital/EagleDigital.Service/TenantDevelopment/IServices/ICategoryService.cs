using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

//using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.TenantDevelopment.IServices
{
     public interface ICategoryService
    {
        IQueryable<Category> List();
        Category Details(int id);
        Category Insert(Category category);
        Category Update(Category category);
    }
}
