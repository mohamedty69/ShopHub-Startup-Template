using myshop.BLL.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface ICartService
    {
        public Task<bool> AddProductToCartAsync(int id);
        public IEnumerable<CartItem> RemoveItemFromCart(int id);
        public IEnumerable<CartItem> IncreaseQuantityOfItem(int id);
        public IEnumerable<CartItem> DecreaseQuantityOfItem(int id);
        public bool DeleteCart();
        public IEnumerable<CartItem> GetCartItems();

    }
}
