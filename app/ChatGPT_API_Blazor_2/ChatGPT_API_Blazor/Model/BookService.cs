using ChatGPT_API_Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatGPT_API_Blazor.Model
{
    public class BookService
    {
        private readonly ChatGPT_API_BlazorContext _context;

        public BookService(ChatGPT_API_BlazorContext context)
        {
            _context = context;
        }

        // 指定された注文IDの注文情報（Book）を取得する
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders

                .Include(b => b.Pizzas)

                .FirstOrDefaultAsync(b => b.OrderId == orderId);
        }

        // 必要に応じて全注文の取得も
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders

                .Include(b => b.Pizzas)

                .ToListAsync();
        }

        // 注文の保存（新規作成）も可能に
        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
