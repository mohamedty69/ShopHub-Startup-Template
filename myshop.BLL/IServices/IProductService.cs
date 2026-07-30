using myshop.BLL.DTOs.Cart;
using myshop.BLL.DTOs.Product.Admin;
using myshop.BLL.DTOs.Product.Customer;
using myshop.BLL.PageList;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IProductService
    {
        public Task<bool> AddProductAsync(CreateProductDTO productDTO);
        public Task<bool> AddProductToCartAsync(int id);
        public Task<IEnumerable<DisplayProductDTO>> GetAllProductsAsync();
        public Task<PageList<DisplayAllProducts>> GetAllProductsForCustomersAsync(string searchWord, string sortColumn, String sortOrder, int page, int pagesize);
        public Task<bool> UpdateProductAsync(EditProductDTO productDTO);
        public Task<EditProductDTO> GetProductByIdAsync(int id);
        public Task<bool> DeleteProductAsync(int id);
    }
}
