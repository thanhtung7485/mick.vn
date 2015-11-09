using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

//using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.TenantDevelopment.IServices
{
    public  interface IDomainInforService
    {
        IQueryable<DomainInfor> List();
        DomainInfor Details(int id);
        DomainInfor Insert(DomainInfor domainInfor);
        DomainInfor Update(DomainInfor domainInfor);
        DomainInfor Delete(int id);
    }
}
