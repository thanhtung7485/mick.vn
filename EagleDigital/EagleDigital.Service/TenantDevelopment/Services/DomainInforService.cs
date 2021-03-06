﻿using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;
using EagleDigital.CodeFirst.TenantDevelopment.Repositories;
using EagleDigital.Service.TenantDevelopment.IServices;
//using EagleDigital.DbFirst.Model;
//using EagleDigital.DbFirst.Repositories;

namespace EagleDigital.Service.TenantDevelopment.Services
{
    public class DomainInforService :IDomainInforService
    {
        private readonly IEntityRepository<DomainInfor> _domainInforRepository;

        public DomainInforService(IEntityRepository<DomainInfor> domainInforRepository)
        {
            _domainInforRepository = domainInforRepository;
        }
            
        public IQueryable<DomainInfor> List()
        {
            var domainInfor = _domainInforRepository.GetAll();
            return domainInfor;
        }

        public DomainInfor Details(int id)
        {
            var domainInforDetails = _domainInforRepository.Get(id);
            return domainInforDetails;
        }

        public DomainInfor Insert(DomainInfor domainInfor)
        {
            var domainDetails = new DomainInfor();
            domainDetails.DomainId = domainInfor.DomainId;
            domainDetails.Content = domainInfor.Content;
            domainDetails.TabNameId = domainInfor.TabNameId;

            domainDetails = _domainInforRepository.InsertOnCommit(domainDetails);
            _domainInforRepository.CommitChanges();
            return domainDetails;
        }

        public DomainInfor Update(DomainInfor domainInfor)
        {
            var domainDetails = _domainInforRepository.Get(domainInfor.Id);
            domainDetails.DomainId = domainInfor.DomainId;
            domainDetails.Content = domainInfor.Content;
            domainDetails.TabNameId = domainInfor.TabNameId;

            _domainInforRepository.CommitChanges();
            return domainDetails;
        }

        public DomainInfor Delete(int id)
        {
            var domainDetails = _domainInforRepository.Get(id);
            _domainInforRepository.DeleteOnCommit(domainDetails);
            _domainInforRepository.CommitChanges();
            return domainDetails;
        }
    }
}
