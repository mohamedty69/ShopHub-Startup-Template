using myshop.BLL.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface ICategoryService
    {
        public Task<bool> CreaetCategoryAsync(CategoryDTO category);
        public Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        public Task<CategoryDTO> GetCategoryByIdAsync(int id);
    }
}
