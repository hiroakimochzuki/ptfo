using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatGPT_API_Blazor.Model;

namespace ChatGPT_API_Blazor.Data
{
    public class ChatGPT_API_BlazorContext : DbContext
    {
        public ChatGPT_API_BlazorContext(DbContextOptions<ChatGPT_API_BlazorContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaSpecial> PizzaSpecials { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book（Order）エンティティに埋め込む設定
            modelBuilder.Entity<Order>()
                .OwnsOne(b => b.DeliveryAddress, a =>
                {
                    a.Property(x => x.Name).HasMaxLength(100).IsRequired();
                    a.Property(x => x.Prefecture).HasMaxLength(50);
                    a.Property(x => x.City).HasMaxLength(50);
                    a.Property(x => x.Street).HasMaxLength(200);
                    a.Property(x => x.PostalCode).HasMaxLength(20);
                    a.Property(x => x.Phone).HasMaxLength(20);
                });

            // Pizza の BasePrice も精度を明示
            modelBuilder.Entity<Pizza>()
                .Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)");

            // PizzaSpecial があれば同様に
            modelBuilder.Entity<PizzaSpecial>()
                .Property(ps => ps.BasePrice)
                .HasColumnType("decimal(18,2)");
        }

    }

}
