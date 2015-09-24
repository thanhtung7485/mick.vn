using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.IServices
{
    public  interface IDomainInforService
    {
        IQueryable<DomainInfor> List();
        DomainInfor Details(int id);
        DomainInfor Insert(DomainInfor domainInfor);
        DomainInfor Update(DomainInfor domainInfor);
    }
}
