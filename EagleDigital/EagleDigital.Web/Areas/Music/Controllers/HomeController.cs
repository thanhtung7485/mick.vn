﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

        public ActionResult Upload()
        {
            var model = new SongModelView();
            var config = new S3Config
            {
                // Unique Policy ID and duration
                Uuid = Guid.NewGuid(),
                ExpirationTime = TimeSpan.FromHours(12),

                // Authentication
                AccessKey = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSAccessKey"].Value,
                SecretAccessKey =System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSSecretKey"].Value,

                // Bucket name and key prefix (folder)
                Bucket =System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSBucket"].Value,
                BucketUrl = string.Format("https://{0}.s3.amazonaws.com/",System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSBucket"].Value),
                KeyPrefix = "upload/",

                // See http://docs.aws.amazon.com/AmazonS3/latest/dev/ACLOverview.html#CannedACL
                Acl = "private",
                S3AmazonUrl = "https://" + "s3-us-west-1" + ".amazonaws.com/" + System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Music/Views/Web.config").AppSettings.Settings["AWSBucket"].Value,

                // Mime type prefix
                ContentTypePrefix = "",//"video/mp4",
                SuccessStatus = "200",
                // Fully qualified URL of an "empty document" in the same origin
                // Required for IE < 10
                // SuccessUrl = "http://localhost:29999/Mix/MixUpload"
            };

            model.Policy = model.GetPolicy(config);
            model.PolicySignature = model.GetSign(model.Policy, config.SecretAccessKey);
            model.S3Config = config;

            ViewBag.Policy = model.Policy;
            ViewBag.PolicySignature = model.PolicySignature;

            return PartialView("_PartialUploadMusic", model);
        }

        [HttpPost]
        public JsonResult Upload(SongModelView model)
        {
            var result =  _songService.Insert(model);
            return Json(new {Status = "OK", Id = result.Id}, JsonRequestBehavior.AllowGet);
            // return PartialView("_PartialUploadMusic", model);
        }

        public ActionResult ListSong()
        {
            var listSong = _songService.List();
            var model = listSong.Select(song => new SongModelView()
                                                    {
                                                        Id = song.Id, Name = song.Name, Link = song.Link, GenreId = song.GenreId, AuthorId = song.AuthorId
                                                    }).ToList();
            return PartialView("_PartialListMusic", model);
        }


        public ActionResult Play(int? id)
        {
            var song = _songService.Details(1);
            var model = new SongModelView()
                            {
                                Id = song.Id,
                                Name = song.Name,
                                Link = song.Link,
                                GenreId = song.GenreId,
                                AuthorId = song.AuthorId
                            };
            return PartialView("_PartialPlay", model);
        }

        public ActionResult ListAuthor()
        {
            var listAuthor = _authorService.List();
            var model = listAuthor.Select(p => new AuthorModelView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
            return PartialView("_PartialListAuthor", model);
        }

        public ActionResult ListGenre()
        {
            var listGenre = _genreService.List();
            var model = listGenre.Select(p => new GenreModelView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
            return PartialView("_PartialListGenre", model);
        }


    }
}
