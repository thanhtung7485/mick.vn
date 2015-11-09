using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EagleDigital.Service.TenantMusic.IServices;
using EagleDigital.Web.Areas.Music.Models;

namespace EagleDigital.Web.Areas.Music.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Music/Home/

        private readonly ISongService _songService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;

        public HomeController(ISongService songService, IGenreService genreService, IAuthorService authorService)
        {
            _songService   = songService;
            _genreService  = genreService;
            _authorService = authorService;
        }


        public ActionResult Index()
        {
            var model = new AllinOneModelView();
            return View(model);
        }

    }
}
