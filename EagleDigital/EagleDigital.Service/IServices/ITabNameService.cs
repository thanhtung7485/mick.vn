using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.CodeFirst.Models;
//using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.IServices
{
    public interface ITabNameService
    {
        IQueryable<TabName> List();
        TabName Details(int id);
        TabName Insert(TabName tabName);
        TabName Update(TabName tabName);
    }
}
