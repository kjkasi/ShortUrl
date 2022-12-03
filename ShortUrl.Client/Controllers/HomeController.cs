using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ShortUrl.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            List<Token> items = await client.GetFromJsonAsync<List<Token>>("http://localhost:5000/Token");
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string originalUrl)
        {
            Token item = new Token
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(item);
            if (ModelState.IsValid)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:5000/Token", item);
                Token? token = await response.Content.ReadFromJsonAsync<Token>();
                return RedirectToAction(actionName: nameof(Details), routeValues: token);
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/Token/{id}");
            return View(item);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/Token/{id}");
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/Token/{id}");
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://localhost:5000/Token/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");//StatusCode(StatusCodes.Status202Accepted, id);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("~/{shortUrl}", Name = "Redirect")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/{shortUrl}");
            return RedirectPermanentPreserveMethod(item.OriginalUrl);
        }
    }
}
