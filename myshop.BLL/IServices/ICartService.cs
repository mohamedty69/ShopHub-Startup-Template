using myshop.BLL.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface ICartService
    {
        public Task<bool> AddProductToCartAsync(int id);
        public Task<bool> RemoveItemFromCartAsync(int id);
        public bool IncreaseQuantityOfItemAsync(int id);
        public Task<bool> DecreaseQuantityOfItemAsync(int id);
        public bool DeleteCart();
        public IEnumerable<CartItem> GetCartItems();

    }
}
