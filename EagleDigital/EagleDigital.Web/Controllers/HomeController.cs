using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EagleDigital.Service.IServices;
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
        private readonly IDomainInforService _domainInforService;

        public HomeController(ICategoryService categoryService, IDomainService domainService, IDomainInforService domainInforService)
        {
            _domainInforService = domainInforService;
            _categoryService = categoryService;
            _domainService = domainService;
        }

        public ActionResult Index()
        {
            var listCategory = _categoryService.List().ToList();
            var listDomain = _domainService.List().ToList();
            var model = new HomeModelView {ListDomain = listDomain, ListCategory = listCategory};
            ViewBag.ListCategory = listCategory;
            if (HttpContext.Session != null) HttpContext.Session.Add("ListCategory", listCategory);
            return View(model);
        }

        public ActionResult DomainDetail(int id)
        {
            var listDomainInfor = _domainInforService.List().Where(p => p.DomainId == id).ToList();
            var model = new DomainInforModel();
            model.ListDomainInfors = listDomainInfor;
            return View(model);
        }


    }
}
