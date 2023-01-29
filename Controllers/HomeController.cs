using Microsoft.AspNetCore.Mvc;
using Sklep_MVC_Projekt.Models;
using Sklep_MVC_Projekt.Services;
using System.Diagnostics;

namespace Sklep_MVC_Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CurrencyService _currencyService;
        private readonly AdminService _adminService;
        private readonly MailService _mailService;

        public HomeController(ILogger<HomeController> logger, CurrencyService currencyService, AdminService adminService, MailService mailService)
        {
            _logger = logger;
            _currencyService = currencyService;
            _adminService = adminService;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //Console.WriteLine(_currencyService.ReindexPrices("GBP"));
            string? visited = HttpContext.Session.GetString("Visited");
            if (visited == null)
            {
                _adminService.IncrementVisitCounter();
                HttpContext.Session.SetString("Visited", "Yes");
            }

            _mailService.SendEmail("<h1>aaaaaaaaa</h1>","NIECH DZIALA", "funkowski.krzysztof@gmail.com", "Krzysztof Funkowski");
            Console.WriteLine(_adminService.GetVisitCounter());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}