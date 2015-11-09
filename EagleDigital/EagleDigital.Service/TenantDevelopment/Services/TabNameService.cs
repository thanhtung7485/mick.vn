using System.Linq;
using EagleDigital.CodeFirst.TenantDevelopment.Models;
using EagleDigital.CodeFirst.TenantDevelopment.Repositories;
using EagleDigital.Service.TenantDevelopment.IServices;
//using EagleDigital.DbFirst.Model;
//using EagleDigital.DbFirst.Repositories;

namespace EagleDigital.Service.TenantDevelopment.Services
{
    public class TabNameService : ITabNameService
    {

        private readonly IEntityRepository<TabName> _tabNameRepository;
        public TabNameService(IEntityRepository<TabName> tabNameRepository)
        {
            _tabNameRepository = tabNameRepository;
        }

        public IQueryable<TabName> List()
        {
            var tabNameList = _tabNameRepository.GetAll().OrderBy(p => p.Name);
            return tabNameList;
        }

        public TabName Details(int id)
        {
            var tabNameDetails = _tabNameRepository.Get(id);
            return tabNameDetails;
        }

        public TabName Insert(TabName tabName)
        {
            var tabNameDetails = new TabName();
            tabNameDetails.Name = tabName.Name;
            tabNameDetails = _tabNameRepository.InsertOnCommit(tabNameDetails);
            _tabNameRepository.CommitChanges();
            return tabNameDetails;
        }

        public TabName Update(TabName tabName)
        {
            var tabNameDetails = _tabNameRepository.Get(tabName.Id);
            tabNameDetails.Name = tabName.Name;
            _tabNameRepository.CommitChanges();
            return tabNameDetails;
        }
    }
}
