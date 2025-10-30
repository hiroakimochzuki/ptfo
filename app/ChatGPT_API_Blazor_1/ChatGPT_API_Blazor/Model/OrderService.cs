using ChatGPT_API_Blazor.Data;
using ChatGPT_API_Blazor.Model;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly ChatGPT_API_BlazorContext _context;

    public OrderService(ChatGPT_API_BlazorContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(b => b.Pizzas)
            .FirstOrDefaultAsync(b => b.OrderId == orderId);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(b => b.Pizzas)
            .ToListAsync();
    }

    public async Task<int> SaveOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order.OrderId;
    }



    // 最新の OrderId を降順で１件取得
    public async Task<Order?> GetLastOrderAsync()
    {
        return await _context.Orders
            .Include(b => b.Pizzas)
            .OrderByDescending(b => b.CreatedTime)
            .FirstOrDefaultAsync();
    }



}
