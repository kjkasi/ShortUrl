using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ShortUrl.Web.Controllers
{
    [Route("{controller}")]
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemController(ILogger<ItemController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("{Action}")]
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("http://host.docker.internal:5000/item");
                response.EnsureSuccessStatusCode();
                List<Item> items = await response.Content.ReadFromJsonAsync<List<Item>>();
                return View(items);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        [HttpGet]
        [Route("{Action}")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{Action}")]
        public async Task<IActionResult> Create(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    var response = await client.PostAsJsonAsync("http://host.docker.internal:5000/item", item);
                    response.EnsureSuccessStatusCode();
                    Item newItem = await response.Content.ReadFromJsonAsync<Item>();
                    return RedirectToRoute(new
                    {
                        controller = "Item",
                        action = "Details",
                        id = newItem.Id
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        [HttpGet]
        [Route("{Action}/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://host.docker.internal:5000/item/{id}");
                response.EnsureSuccessStatusCode();
                Item item = await response.Content.ReadFromJsonAsync<Item>();
                return View(item);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        [HttpGet]
        [Route("{Action}/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://host.docker.internal:5000/item/{id}");
                response.EnsureSuccessStatusCode();
                Item item = await response.Content.ReadFromJsonAsync<Item>();
                return View(item);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{Action}")]
        public async Task<IActionResult> DeleteItem(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = _httpClientFactory.CreateClient();
                    var response = await client.DeleteAsync($"http://host.docker.internal:5000/item/{id}");
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://host.docker.internal:5000/item/{shortUrl}");
                response.EnsureSuccessStatusCode();
                Item item = await response.Content.ReadFromJsonAsync<Item>();
                return Redirect(item.OriginalUrl);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");

        }
    }
}
