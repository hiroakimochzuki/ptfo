namespace ChatGPT_API_Blazor.Model
{
    public class PizzaSpecial
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string GetFormattedBasePrice() => BasePrice.ToString("0.00");
    }
}

