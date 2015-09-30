using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.Models;
using EagleDigital.CodeFirst.Repositories;
//using EagleDigital.DbFirst.Model;
//using EagleDigital.DbFirst.Repositories;
using EagleDigital.Service.IServices;

namespace EagleDigital.Service.Services
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
