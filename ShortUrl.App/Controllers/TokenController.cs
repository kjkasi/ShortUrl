using Microsoft.AspNetCore.Mvc;
using ShortUrl.App.Dtos;
using ShortUrl.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenRepository _repository;

        public TokenController(ITokenRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetItemById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(TokenCreateDto item)
        {
            var TokenCreateDto = await _repository.CreateToken(item);
            return Ok(TokenCreateDto);
        }
    }
}
