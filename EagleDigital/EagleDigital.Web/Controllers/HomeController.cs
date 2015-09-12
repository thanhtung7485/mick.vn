using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EagleDigital.Service.Services;
using EagleDigital.Web.Models;

namespace EagleDigital.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private readonly ICategoryService _categoryService;
        private readonly IDomainService _domainService;

        public HomeController(ICategoryService categoryService, IDomainService domainService)
        {
            _categoryService = categoryService;
            _domainService = domainService;
        }

        public ActionResult Index()
        {
            var listCategory = _categoryService.List().ToList();
            var listDomain = _domainService.List().ToList();
            var model = new HomeModelView {ListDomain = listDomain, ListCategory = listCategory};
            return View(model);
        }

        public ActionResult DomainDetail()
        {
            return View();
        }


    }
}
