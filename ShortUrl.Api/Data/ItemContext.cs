using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.Models;
using ShortUrl.Api.Services;

namespace ShortUrl.Api.Data
{
    public class ItemContext : DbContext
    {
        private readonly IAliasService _aliasService;

        public ItemContext(DbContextOptions<ItemContext> options, IAliasService aliasService)
            : base(options)
        {
            _aliasService = aliasService;
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    OriginalUrl = "http://ya.ru",
                    ShortUrl = _aliasService.ConfusionConvert(1)
                },
                new Item
                {
                    Id = 2,
                    OriginalUrl = "http://localhost:5001/home/privacy",
                    ShortUrl = _aliasService.ConfusionConvert(2)
                },
                new Item
                {
                    Id = 3,
                    OriginalUrl = "https://translate.yandex.ru/",
                    ShortUrl = _aliasService.ConfusionConvert(3)
                }
            );
        }
    }
}
