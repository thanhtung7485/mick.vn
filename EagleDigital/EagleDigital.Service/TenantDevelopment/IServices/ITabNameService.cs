using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

//using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.TenantDevelopment.IServices
{
    public interface ITabNameService
    {
        IQueryable<TabName> List();
        TabName Details(int id);
        TabName Insert(TabName tabName);
        TabName Update(TabName tabName);
    }
}
