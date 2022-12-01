using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShortUrl.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            //var client = new HttpClient();
            //var items
            _client.GetAsync("localhost:5000/Api/v1/Token/3");
            return View();
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
