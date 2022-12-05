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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            List<Item> items = await client.GetFromJsonAsync<List<Item>>("http://host.docker.internal:5000/GetItems");
            return View(items);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("Create")]
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
                var response = await client.PostAsJsonAsync("http://host.docker.internal:5000/AddItem", item);

                Item newItem = await response.Content.ReadFromJsonAsync<Item>();
                return RedirectToAction(actionName: nameof(Details), routeValues: newItem);
            }

            return View();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetTokenById/{id}");
            return View(item);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetTokenById/{id}");
            return View(item);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"http://host.docker.internal:5000/DeleteItem/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetTokenByUrl/{shortUrl}");
            return Redirect(item.OriginalUrl);
        }

        [HttpGet]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
