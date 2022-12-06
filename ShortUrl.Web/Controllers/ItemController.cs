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
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                List<Item> items = await client.GetFromJsonAsync<List<Item>>("http://host.docker.internal:5000/GetItems");
                return View(items);
            }
            catch(HttpRequestException ex )
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<Item>());
            }
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

            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://host.docker.internal:5000/AddItem", item);

                Item newItem = await response.Content.ReadFromJsonAsync<Item>();
                return RedirectToAction(actionName: nameof(Details), routeValues: newItem);
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new Item());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetItemById/{id}");
                return View(item);
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new Item());
            }
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetItemById/{id}");
                return View(item);
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new Item());
            }

        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                var response = await client.DeleteAsync($"http://host.docker.internal:5000/DeleteItem/{id}");
                return RedirectToAction("Index");
            }
            catch(HttpRequestException ex) 
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<IActionResult> GetTokenByUrl(string shortUrl)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                Item item = await client.GetFromJsonAsync<Item>($"http://host.docker.internal:5000/GetTokenByUrl/{shortUrl}");
                return Redirect(item.OriginalUrl);
            }
            catch(HttpRequestException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

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
