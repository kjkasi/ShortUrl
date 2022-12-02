using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.App.Dtos;
using ShortUrl.App.Models;
using ShortUrl.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenRepository _repository;

        public TokenController(ITokenRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTokens()
        {
            var itemList = await _repository.GetAllTokens();
            return Ok(itemList);
        }

        [HttpGet("/{shortUrl}", Name = "GetTokenByUrl")]
        public async Task<ActionResult> GetTokenByUrl(string shortUrl)
        {
            var token =  await _repository.GetTokenByUrl(shortUrl);
            return Ok(token);
        }

        [HttpGet("{id:int}", Name = "GetTokenById")]
        public async Task<ActionResult> GetTokenById(int id)
        {
            var token = await _repository.GetTokenById(id);
            return Ok(token);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(Token item)
        {
            var token = await _repository.CreateToken(item);
            return StatusCode(StatusCodes.Status201Created, token);
        }
    }
}
