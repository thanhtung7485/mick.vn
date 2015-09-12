using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EagleDigital.DbFirst.Model;

namespace EagleDigital.Web.Models
{
    public class HomeModelView
    {
        public List<Domain> ListDomain { get; set; } 
        public List<Category> ListCategory { get; set; } 
    }
}