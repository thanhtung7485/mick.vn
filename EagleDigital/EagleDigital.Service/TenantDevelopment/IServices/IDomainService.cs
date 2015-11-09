using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

//using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.TenantDevelopment.IServices
{
    public interface IDomainService
    {
        IQueryable<Domain> List();
        Domain Details(int id);
        Domain Insert(Domain domain);
        Domain Update(Domain domain);
    }
}
