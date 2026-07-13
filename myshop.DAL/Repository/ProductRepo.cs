using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Repository
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly ApplicationDbContext _context;
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override Task<bool> Add(Product entity)
        {
            return base.Add(entity);
        }
        public override async Task<IEnumerable<Product>> GetAll()
        {
            var listOfProduct = await _context.Products
                .Include(c => c.Category)
                .ToListAsync();
            return listOfProduct;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product?? throw new NullReferenceException("The product not found");
        }

        public override async Task<bool> Update(Product entity)
        {
            var existingProduct = await GetProductByIdAsync(entity.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = entity.Name;
                existingProduct.Description = entity.Description;
                existingProduct.Img = entity.Img;
                existingProduct.Price = entity.Price;
                existingProduct.CategoryId = entity.CategoryId;
                existingProduct.Category = entity.Category;
                return true;
            }
            return false;
        }
        public override Task<bool> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
