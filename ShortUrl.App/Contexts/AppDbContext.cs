using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Models.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Token> TokenItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Token>().HasData(
                new Token
                {
                    Id = 1,
                    OriginalUrl = "http://ya.ru",
                    ShortUrl = "3"
                },
                new Token
                {
                    Id = 2,
                    OriginalUrl = "ay.ru",
                    ShortUrl = "4"
                }
            );
        }
    }
}
