using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Net;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()

        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Klubid> klubid = new List<Klubid>();

            var web = new HtmlWeb();
            var doc = web.Load("https://jalgpall.ee/voistlused/madalamad-liigad/5/III.E");
            var doc2 = web.Load("https://jalgpall.ee/voistlused/5/team/4223?season=2022");

            foreach (var item in doc.DocumentNode.SelectNodes("//*[@id=\"page\"]/div/div[1]/div[1]/div[1]/table/tbody/tr"))
            {
          
                string kohtliigas = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string img = item.SelectSingleNode($".//td[2]/img").GetAttributeValue("src", null).Trim();
                string title = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string mänge = item.SelectSingleNode(".//td[4]").InnerText.Trim();
                string võite = item.SelectSingleNode(".//td[5]").InnerText.Trim();
                string viike = item.SelectSingleNode(".//td[6]").InnerText.Trim();
                string kaotusi = item.SelectSingleNode(".//td[7]").InnerText.Trim();
                string väravaid = item.SelectSingleNode(".//td[8]").InnerText.Trim();
                string punkte = item.SelectSingleNode(".//td[9]").InnerText.Trim();
           
                klubid.Add(new Klubid()
                {
                    title = title,
                    kohtliigas = kohtliigas,
                    logo = img,
                    võite = võite,
                    viike = viike,
                    kaotusi = kaotusi,
                    väravaid = väravaid,
                    punkte = punkte,
                    mänge = mänge

                });

           
            }

         

            foreach (var item in doc2.DocumentNode.SelectNodes("//*[@id=\"page\"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr").Take(4))
            {
           
                string date = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string teamleft = item.SelectSingleNode($".//td[2]/div/img").GetAttributeValue("src", null).Trim();
                string teamright = item.SelectSingleNode($".//td[4]/div/img").GetAttributeValue("src", null).Trim();
                string score = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string score6 = item.SelectSingleNode(".//td").InnerText.Trim();

                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[44]/td
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[3]/td[4]/div/img
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[1]/td[4]/div/img
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[1]/td[3]/a
                klubid.Add(new Klubid()
                {
                    date = date,
                    teamleft = teamleft,
                    teamright = teamright,
                    score = score


                });


            }

            return View(klubid);

            //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[1]


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}