using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;
using EagleDigital.CodeFirst.TenantDevelopment.Repositories;
using EagleDigital.Service.TenantDevelopment.IServices;

//using EagleDigital.DbFirst.Model;
//using EagleDigital.DbFirst.Repositories;

namespace EagleDigital.Service.TenantDevelopment.Services
{
    public  class DomainService : IDomainService
    {

        private readonly IEntityRepository<Domain> _domainRepository;
        public DomainService(IEntityRepository<Domain> domainRepository)
        {
            this._domainRepository = domainRepository;
        }

        public IQueryable<Domain> List()
        {
            var domainList = _domainRepository.GetAll().OrderBy(p => p.Name);
            return domainList;
        }

        public Domain Details(int id)
        {
            var domainDetails = _domainRepository.Get(id);
            return domainDetails;
        }

        public Domain Insert(Domain domain)
        {
            var domainDetails = new Domain();
            domainDetails.Name = domain.Name;
            domainDetails.Description = domain.Description;
            domainDetails.SubCategoryId = domain.SubCategoryId;

            domainDetails = _domainRepository.InsertOnCommit(domainDetails);
            _domainRepository.CommitChanges();
            return domainDetails;
        }

        public Domain Update(Domain domain)
        {
            var domainDetails = _domainRepository.Get(domain.Id);
            domainDetails.Name = domain.Name;
            domainDetails.Description = domain.Description;
            domainDetails.SubCategoryId = domain.SubCategoryId;
            _domainRepository.CommitChanges();
            return domainDetails;
        }
    }
}
