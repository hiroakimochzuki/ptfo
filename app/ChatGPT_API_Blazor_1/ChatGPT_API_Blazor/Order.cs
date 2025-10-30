namespace ChatGPT_API_Blazor.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public Address DeliveryAddress { get; set; }
        public List<Pizza> Pizzas { get; set; }

        public Order()
        {
            DeliveryAddress = new Address();
            Pizzas = new List<Pizza>();
        }

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

        // ここを追加
        public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");

        // またはプロパティとして
        public string FormattedTotalPrice => GetTotalPrice().ToString("0.00");
    }
}