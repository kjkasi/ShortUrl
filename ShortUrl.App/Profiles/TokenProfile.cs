using AutoMapper;
using ShortUrl.App.Dtos;
using ShortUrl.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Profiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            // Source -> Target
            CreateMap<Token, TokenCreateDto>();
            CreateMap<TokenCreateDto, Token>();
            CreateMap<Token, TokenReadDto>();
        }
    }
}
