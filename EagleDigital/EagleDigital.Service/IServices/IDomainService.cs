using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleDigital.DbFirst.Model;

namespace EagleDigital.Service.Services
{
    public interface IDomainService
    {
        IQueryable<Domain> List();
        Domain Details(int id);
        Domain Insert(Domain domain);
        Domain Update(Domain domain);
    }
}
