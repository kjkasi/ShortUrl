using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ShortUrl.Client.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            HttpClient client = new HttpClient();

            List<Token> items = await client.GetFromJsonAsync<List<Token>>("http://localhost:5000/Token");

            return View(items);
        }

        public async Task<IActionResult> Create(string originalUrl)
        {
            Token item = new Token
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(item);
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync<Token>("http://localhost:5000/Token", item);
                Token? token = await response.Content.ReadFromJsonAsync<Token>();
                return RedirectToAction(actionName: nameof(Show), routeValues: token);
            }

            return View();
        }

        public async Task<IActionResult> Show(Token token)
        {
            //HttpClient client = new HttpClient();
            //Token item = await client.GetFromJsonAsync<Token>("http://localhost:5000/Token");
            return View(token);
        }

        [HttpGet("{Action}/{originalUrl:string}")]
        public IActionResult Redirect(string originalUrl)
        {
            return Redirect(originalUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        /*
        [HttpGet("{shortUrl}", Name = "GetTokenByUrl")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            HttpClient client = new HttpClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/{shortUrl}");
            //return Ok(item);
            return RedirectPermanent("http://www.ya.ru");
            //return Redirect("redirect");
            //return RedirectToAction(actionName: nameof(GetTokenById), routeValues: token);
        }
        */
    }
}
