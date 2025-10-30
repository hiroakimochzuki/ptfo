using System.Collections.Generic;

namespace ChatGPT_API_Blazor.Model
{
    /// <summary>
    /// Represents a customized pizza as part of an order
    /// </summary>
    public class Pizza
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 9;
        public const int MaximumSize = 17;

        public int Id { get; set; }

        public int OrderId { get; set; }

        public int SpecialId { get; set; }
        public PizzaSpecial Special { get; set; } = new(); // ★ null防止のため初期化

        public int Size { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int Quantity { get; set; } = 1;

        public List<PizzaTopping> Toppings { get; set; } = new(); // ★ 任意

        public decimal GetTotalPrice() => BasePrice * Quantity;

        public decimal GetBasePrice()
        {
            return ((decimal)Size / DefaultSize) * Special.BasePrice;
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
