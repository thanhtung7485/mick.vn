using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.DbFirst.Model;
using EagleDigital.DbFirst.Repositories;

namespace EagleDigital.Service.Services
{
    public class CategoryService : ICategoryService
    {
       private readonly IEntityRepository<Category> _categoryRepository;
       public CategoryService(IEntityRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public IQueryable<Category> List()
        {
            var bulletinList = _categoryRepository.GetAll().OrderBy(p=>p.Name);
            return bulletinList;
        }

        public Category Details(int id)
        {
            var cateogryDetails = _categoryRepository.Get(id);
            return cateogryDetails;
        }

        public Category Insert(Category category)
        {
            var cateogryDetails = new Category();
            cateogryDetails.Name = category.Name;
            cateogryDetails.Description = category.Description;

            cateogryDetails = _categoryRepository.InsertOnCommit(cateogryDetails);
            _categoryRepository.CommitChanges();
            return cateogryDetails;
        }

        public Category Update(Category category)
        {
            var cateogryDetails = _categoryRepository.Get(category.Id);
            cateogryDetails.Name = category.Name;
            cateogryDetails.Description = category.Description;
            _categoryRepository.CommitChanges();
            return cateogryDetails;
        }
    }
}
