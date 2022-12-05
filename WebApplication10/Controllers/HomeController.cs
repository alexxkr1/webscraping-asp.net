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
            var doc = web.Load("https://jalgpall.ee/voistlused/52/premium-liiga");

            foreach (var item in doc.DocumentNode.SelectNodes("//*[@id=\"page\"]/div/div[1]/div[1]/div[1]/table/tbody/tr"))
            {
                int i = 1;
                string kohtliigas = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string img = item.SelectSingleNode($".//td[2]/img").GetAttributeValue("src", null).Trim();
                string title = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string mänge = item.SelectSingleNode(".//td[4]").InnerText.Trim();
                string võite = item.SelectSingleNode(".//td[5]").InnerText.Trim();
                string viike = item.SelectSingleNode(".//td[6]").InnerText.Trim();
                string kaotusi = item.SelectSingleNode(".//td[7]").InnerText.Trim();
                string väravaid = item.SelectSingleNode(".//td[8]").InnerText.Trim();
                string punkte = item.SelectSingleNode(".//td[9]").InnerText.Trim();
   

                //string img = item.SelectSingleNode($"//*[@id='page']/div/div[1]/div[1]/div[1]/table/tbody/tr/td[{number}]/img").GetAttributeValue("src", null).Trim();
                //string kohtliigas = item.SelectSingleNode($"//*[@id=\"page\"]/div/div[1]/div[1]/div[1]/table/tbody/[{i++}]/td[1]").InnerText.Trim();

                Console.WriteLine(item.InnerText);
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
            foreach (var item in doc.DocumentNode.SelectNodes("//*[@id=\"page\"]/div/div[1]/div[1]/div[1]/table/tbody/tr/td[1]"))
            {
                klubid.Add(new Klubid()
                {
                    //kohtliigas = item.InnerText
            });
        }

            return View(klubid);
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