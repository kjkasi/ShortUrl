using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortUrl.Api.Models;
using ShortUrl.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShortUrl.Api.Controllers
{
    [ApiController]
    [Route("item")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<ActionResult> GetItems()
        {
            var items = await _repository.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        [Route("{shortUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult> GetItemByUrl(string shortUrl)
        {
            var item = await _repository.GetItemByUrl(shortUrl);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult> GetItemById(int id)
        {

            var item = await _repository.GetItemById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult> AddItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _repository.CreateItem(item);
            return Ok(item);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _repository.GetItemById(id);
            if (item is null)
            {
                return NotFound();
            }

            var isSuccess = await _repository.DeleteItem(item);
            return Ok(isSuccess);
        }
    }
}
