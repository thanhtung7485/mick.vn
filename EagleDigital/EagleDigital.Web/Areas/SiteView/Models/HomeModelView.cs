using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EagleDigital.CodeFirst.TenantDevelopment.Models;

namespace EagleDigital.Web.Areas.SiteView.Models
{
    public class HomeModelView
    {
        public List<Domain> ListDomain { get; set; }
        public List<Category> ListCategory { get; set; } 
    }
}