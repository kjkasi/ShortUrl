using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ShortUrl.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemController(ILogger<ItemController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            //HttpClient client = _httpClientFactory.CreateClient();
            //List<Item> items = await client.GetFromJsonAsync<List<Item>>("http://host.docker.internal:5000/item");
            //return View(items);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Items()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            List<Item> items = await client.GetFromJsonAsync<List<Item>>("http://host.docker.internal:5000/item");
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
            Item item = new Item
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(item);
            if (ModelState.IsValid)
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://host.docker.internal:5000/item", item);

                Item newItem = await response.Content.ReadFromJsonAsync<Item>();
                return RedirectToAction(actionName: nameof(Details), routeValues: newItem);
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/item/{id}");
            return View(item);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/item/{id}");
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/item/{id}");
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://host.docker.internal:5000/item/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Items");
            }
            return RedirectToAction("Error");
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
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/{shortUrl}");
            return RedirectPermanentPreserveMethod(item.OriginalUrl);
        }
    }
}
