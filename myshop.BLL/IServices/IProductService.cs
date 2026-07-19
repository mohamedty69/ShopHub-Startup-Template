using myshop.BLL.DTOs.Product.Customer;
using myshop.BLL.DTOs.Product.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using myshop.BLL.DTOs.Cart;

namespace myshop.BLL.IServices
{
    public interface IProductService
    {
        public Task<bool> AddProductAsync(CreateProductDTO productDTO);
        public Task<bool> AddProductToCartAsync(int id);
        public Task<IEnumerable<DisplayProductDTO>> GetAllProductsAsync();
        public Task<IEnumerable<DisplayAllProducts>> GetAllProductsForCustomersAsync();
        public IEnumerable<CartItem> GetCartItems();
        public Task<bool> UpdateProductAsync(EditProductDTO productDTO);
        public Task<EditProductDTO> GetProductByIdAsync(int id);
        public Task<bool> DeleteProductAsync(int id);
    }
}
