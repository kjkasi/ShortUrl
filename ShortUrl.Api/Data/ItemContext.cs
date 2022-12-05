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
                    ShortUrl = _aliasService.ConfusionConvert(1),
                    FileName = "placeholder.png"
                },
                new Item
                {
                    Id = 2,
                    OriginalUrl = "http://localhost:5001/privacy",
                    ShortUrl = _aliasService.ConfusionConvert(2),
                    FileName = "placeholder.png"
                },
                new Item
                {
                    Id = 3,
                    OriginalUrl = "https://translate.yandex.ru/",
                    ShortUrl = _aliasService.ConfusionConvert(3),
                    FileName = "placeholder.png"
                }
            );
        }
    }
}
