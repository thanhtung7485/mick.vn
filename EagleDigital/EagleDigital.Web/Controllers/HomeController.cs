using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EagleDigital.Service.Services;

namespace EagleDigital.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var list = _categoryService.List().ToList();
            return View(list);
        }

    }
}
