using System.Web.Mvc;

namespace EagleDigital.Web.Areas.Music
{
    public class MusicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Music";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Music_default",
                "Music/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "EagleDigital.Web.Areas.Music.Controllers" }
            );


           // context.MapRoute(
           //    "Admin_default",
           //    "Admin/{controller}/{action}/{id}",
           //    new { controller = "DomainInfor", action = "Index", id = UrlParameter.Optional },
           //    new[] { "EagleDigital.Web.Areas.Admin.Controllers" }
           //);

        }
    }
}
