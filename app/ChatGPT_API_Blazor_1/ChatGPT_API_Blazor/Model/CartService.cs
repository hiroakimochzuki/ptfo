using System.Collections.Generic;
using System.Linq;

namespace ChatGPT_API_Blazor.Model
{
    public class CartService
    {
        private readonly List<CartItem> cartItems = new();

        /// <summary>
        /// カートにアイテムを追加
        /// </summary>
        public void AddToCart(PizzaSpecial special)
        {
            var existing = cartItems.FirstOrDefault(x => x.Pizza.Id == special.Id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    Pizza = special,
                    Quantity = 1
                });
            }
        }

        /// <summary>
        /// カート内のアイテム数（個数合計）
        /// </summary>
        public int GetItemCount() => cartItems.Sum(x => x.Quantity);

        /// <summary>
        /// カート内のアイテム一覧を取得
        /// </summary>
        public List<CartItem> GetCartItems() => cartItems;

        /// <summary>
        /// 合計金額を取得
        /// </summary>
        public decimal GetTotalAmount()
        {
            return cartItems.Sum(p => p.Pizza.BasePrice * p.Quantity);
        }

        /// <summary>
        /// カートを空にする
        /// </summary>
        public void Clear()
        {
            cartItems.Clear();
        }

        public void UpdateQuantity(int pizzaId, int newQuantity)
        {
            var item = cartItems.FirstOrDefault(x => x.Pizza.Id == pizzaId);
            if (item != null)
            {
                if (newQuantity <= 0)
                    cartItems.Remove(item);
                else
                    item.Quantity = newQuantity;
            }
        }

        public void RemoveItem(int pizzaId)
        {
            var item = cartItems.FirstOrDefault(x => x.Pizza.Id == pizzaId);
            if (item != null)
            {
                cartItems.Remove(item);
            }
        }
    }
}