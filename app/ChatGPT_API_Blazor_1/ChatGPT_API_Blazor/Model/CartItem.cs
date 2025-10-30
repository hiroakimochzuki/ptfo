namespace ChatGPT_API_Blazor.Model
{
    public class CartItem
    {
        public int Id { get; set; }  // 主キーを追加
        public PizzaSpecial Pizza { get; set; } = new PizzaSpecial();
        public int Quantity { get; set; }
    }
}
