using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatGPT_API_Blazor.Model
{
    public class Order_memory
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedTime { get; set; }

        public Address DeliveryAddress { get; set; } = new Address();

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
    }
}
