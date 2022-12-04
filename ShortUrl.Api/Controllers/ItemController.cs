using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Api.Models;
using ShortUrl.Api.Repositories;
using System.Threading.Tasks;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemRepository _repository;

        public ItemController(ILogger<ItemController> logger, IItemRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTokens()
        {
            var itemList = await _repository.GetAllItems();
            return Ok(itemList);
        }

        [HttpGet("/{shortUrl}", Name = "GetTokenByUrl")]
        public async Task<ActionResult> GetTokenByUrl(string shortUrl)
        {
            var item = await _repository.GetItemByUrl(shortUrl);
            
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }

            return Ok(item);
        }

        [HttpGet("{id:int}", Name = "GetTokenById")]
        public async Task<ActionResult> GetTokenById(int id)
        {
            var item = await _repository.GetItemById(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(Item item)
        {
            var newItem = await _repository.CreateItem(item);
            return StatusCode(StatusCodes.Status201Created, newItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var result = await _repository.DeleteItem(id);
            return Ok(result);
        }
    }
}
