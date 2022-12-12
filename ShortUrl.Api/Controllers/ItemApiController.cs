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
    [Route("item")]
    [ApiController]
    public class ItemApiController : ControllerBase
    {
        private readonly ILogger<ItemApiController> _logger;
        private readonly IItemRepository _repository;

        public ItemApiController(ILogger<ItemApiController> logger, IItemRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                var items = await _repository.GetAllItems();
                return StatusCode(StatusCodes.Status200OK, items);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpGet]
        [Route("{shortUrl}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetItemByUrl(string shortUrl)
        {
            try
            {
                var item = await _repository.GetItemByUrl(shortUrl);
                if (item is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, null);
                }
                return StatusCode(StatusCodes.Status200OK, item);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetItemById(int id)
        {
            try
            {
                var item = await _repository.GetItemById(id);
                if (item is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, null);
                }
                return StatusCode(StatusCodes.Status200OK, item);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddItem(Item item)
        {
            try
            {
                var newItem = await _repository.CreateItem(item);
                return StatusCode(StatusCodes.Status201Created, newItem);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                var result = await _repository.DeleteItem(id);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }
    }
}
