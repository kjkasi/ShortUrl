using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.Models;

namespace ShortUrl.Api.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    OriginalUrl = "http://ya.ru",
                    ShortUrl = "3"
                },
                new Item
                {
                    Id = 2,
                    OriginalUrl = "http://localhost:5001/home/privacy",
                    ShortUrl = "4"
                },
                new Item
                {
                    Id = 3,
                    OriginalUrl = "https://translate.yandex.ru/",
                    ShortUrl = "5"
                }
            );
        }
    }
}
