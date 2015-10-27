using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EagleDigital.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public class NewTimes
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public string ThanSau(List<NewTimes> list,string content)
        {
            //var abc = list.Select(item => content.Replace(item.Title, "https://"+item.Id));
            //return abc;
            return list.Aggregate(content, (current, item) => current.Replace(item.Title, "https://" + item.Id));
        }

        public ActionResult Index()
        {
            //var listNewTimes = new List<NewTimes>
            //                       {
            //                           new NewTimes(){Id = 1 ,Title = "fake",Content = "Olala Ebula fick"}, // p
            //                            new NewTimes(){Id = 3 ,Title = "Ebula",Content = "Angula"},
            //                           new NewTimes(){Id = 2 ,Title = "fick",Content = "hoangvh"},  
                                      
            //                       };

            var listNewTimes = new List<NewTimes>()
        {
            new NewTimes(){Id = 1,Title = "Nhu Tien", Content = "Vu Nhu Tien dua ra 1 bai bat chu Mick Ar lam trong 1 tieng dong ho bang ngon ngu C#"},
            new NewTimes(){Id = 2, Title = "Mick Ar", Content = "Mick ta thong thao Linq va quyet dinh lam thu"},
            new NewTimes(){Id = 3,Title = "ngon ngu C#", Content = "Ket qua cuoi cung ra sao, lieu Mick Ar co the hoan thanh bai test ma Nhu Tien de ra hay ko"}
        };

            var abddc =
                listNewTimes.Where( 
                    e => e.Id==1 && listNewTimes.Any(c => c.Id != e.Id  && e.Content.ToLower().Contains(c.Title.ToLower()))).ToList();

            var abcd = listNewTimes.Where(t => listNewTimes.Any(c =>  c.Content.ToLower().Contains(t.Title.ToLower()))).ToList();

         
            listNewTimes = listNewTimes.Select(p => new NewTimes()
                                                        {
                                                            Id = p.Id,
                                                            Content  = listNewTimes.Where(t => listNewTimes.Any(c => c.Content.ToLower().Contains(t.Title.ToLower()))).ToList().
                                                            Aggregate(p.Content, (current, item) => current.Replace(item.Title,"<a href='"+item.Id+"'>"+item.Title+"</a>")),
                                                            Title = p.Title
                                                        }).ToList();

            


         //   string delimeter = ",";


            // .ForEach(x=> p.Content=p.Content.Replace(x.Title, "https:abc//")),
            //Content = p.Content + ' ' +
            //   listNewTimes.Where(c => c.Id != p.Id).Aggregate((i, j) => new NewTimes { Title = (i.Title + ' ' + j.Title) }).Title,

            List<string> items = new List<string>() { "foo", "boo", "john", "doe" };
        //    var abc = items.Aggregate((i, j) => i + delimeter + j);
            // Console.WriteLine(items.Aggregate((i, j) => i + delimeter + j));




            return RedirectToAction("Index", "Home", new { area = "SiteView" });
            return View();
        }

    }
}
