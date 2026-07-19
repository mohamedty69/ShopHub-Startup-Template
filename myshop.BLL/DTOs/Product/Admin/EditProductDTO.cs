using myshop.BLL.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.Product.Admin
{
    public class EditProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0.00M;
        public int CategoryId { get; set; }
        public List<CategoryDTO> categories { get; set; } = new List<CategoryDTO>();
    }
}
