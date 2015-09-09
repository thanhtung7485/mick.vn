using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.Services
{
     public interface ICategoryService
    {
        IQueryable<Category> List();
        Category Details(int id);
        Category Insert(Category category);
        Category Update(Category category);
    }
}
