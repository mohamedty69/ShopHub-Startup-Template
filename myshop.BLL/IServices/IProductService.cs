using myshop.BLL.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IProductService
    {
        public Task<bool> AddProductAsync(CreateProductDTO productDTO);
        public Task<IEnumerable<DisplayProductDTO>> GetAllProductsAsync();
        public Task<bool> UpdateProductAsync(EditProductDTO productDTO);
        public Task<EditProductDTO> GetProductByIdAsync(int id);
        public Task<bool> DeleteProductAsync(int id);
    }
}
