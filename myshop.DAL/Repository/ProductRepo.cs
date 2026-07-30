using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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
            return product ?? throw new NullReferenceException("The product not found");
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

        public async Task<List<Product>> GetProductWithPagination(string searchWord, string sortColumn, string sortOrder ,int page, int pageSize)
        {
            var product = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                 product = product.Where(p => p.Name.Contains(searchWord));
            }
            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                if (sortOrder == "desc")
                    product = product.OrderByDescending(GetSortName(sortColumn));
                product = product.OrderBy(GetSortName(sortColumn));
            }
            var productAfterQueries = await product.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return productAfterQueries;

        }

        public Expression<Func<Product, object>> GetSortName(string sortColumn) 
        {
            switch (sortColumn.ToLower())
            {
                case "name":
                        return Product => Product.Name;
                case "price":
                     return Product => Product.Price;
            }
            return null;
        }
        
    }
}
