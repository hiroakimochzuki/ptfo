using ChatGPT_API_Blazor.Model;

public class PizzaTopping
{
    public int Id { get; set; }

    public int ToppingId { get; set; }
    public int PizzaId { get; set; }

    public Pizza Pizza { get; set; } = default!; // ★追加
}
