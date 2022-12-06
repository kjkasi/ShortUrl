using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Api.Models;
using ShortUrl.Api.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
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
        [Route("GetItems")]
        [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetItems()
        {
            var itemList = await _repository.GetAllItems();
            return Ok(itemList);
        }

        [HttpGet]
        [Route("GetItemByUrl/{shortUrl}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetItemByUrl(string shortUrl)
        {
            var item = await _repository.GetItemByUrl(shortUrl);
            
            if (item is null)
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }

            return Ok(item);
        }

        [HttpGet]
        [Route("GetItemById/{id:int}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetItemById(int id)
        {
            var item = await _repository.GetItemById(id);
            if (item is null)
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("AddItem")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> AddItem(Item item)
        {
            var newItem = await _repository.CreateItem(item);
            return StatusCode(StatusCodes.Status201Created, newItem);
        }

        [HttpDelete]
        [Route("DeleteItem/{id:int}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var result = await _repository.DeleteItem(id);
            return Ok(result);
        }
    }
}
