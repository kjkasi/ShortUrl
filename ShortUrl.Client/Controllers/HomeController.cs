﻿using Microsoft.AspNetCore.Mvc;
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
            HttpClient client = _httpClientFactory.CreateClient();

            List<Token> items = await client.GetFromJsonAsync<List<Token>>("http://localhost:5000/Token");

            return View(items);
        }

        /*
        public async Task<IActionResult> List(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            List<Token> items = await client.GetFromJsonAsync<List<Token>>("http://localhost:5000/Token");

            return View(items);
        }
        */


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
                return RedirectToAction(actionName: nameof(Show), routeValues: token);
            }

            return View();
        }

        public async Task<IActionResult> Show(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/Token/{id}");
            return View(item);
        }

        /*
        public async Task<IActionResult> Show(string shortUrl)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/Token/{shortUrl}");
            return View(item);
        }
        */

        /*
        [HttpGet("{Action}/{originalUrl}")]
        public IActionResult Redirect(string originalUrl)
        {
            var url = new Url(originalUrl);
            Console.WriteLine(originalUrl);
            Console.WriteLine(url);
            return RedirectPermanentPreserveMethod(originalUrl);
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpGet("{shortUrl}", Name = "GetTokenByUrl")]
        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Token item = await client.GetFromJsonAsync<Token>($"http://localhost:5000/{shortUrl}");
            return RedirectPermanentPreserveMethod(item.OriginalUrl);
        }
    }
}
