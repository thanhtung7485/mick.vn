using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EagleDigital.CodeFirst.TenantMusic.Models;

namespace EagleDigital.Web.Areas.Music.Models
{
    public class AllinOneModelView
    {
        public Song Song { get; set; }
        public Genre Genre { get; set; }
        public Author Author { get; set; }
        public List<Song> Songs { get; set; } 
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}