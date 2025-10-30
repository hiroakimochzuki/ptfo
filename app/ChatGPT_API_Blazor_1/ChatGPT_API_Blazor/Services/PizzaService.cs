using ChatGPT_API_Blazor.Model;
using ChatGPT_API_Blazor.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatGPT_API_Blazor.Services
{
    public class PizzaService
    {
        private readonly ChatGPT_API_BlazorContext _context;

        public PizzaService(ChatGPT_API_BlazorContext context)
        {
            _context = context;
        }

        public async Task<List<Pizza>> GetPizzasAsync()
        {
            return await _context.Pizzas
                .Include(p => p.Special)
                .Include(p => p.Toppings)
                .ToListAsync();
        }
    }
} 