using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace myshop.DAL.Repository
{
    public class CategoryRepo : GenericRepo<Category> , ICategoryRepo
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override Task<bool> Add(Category entity)
        {
            return base.Add(entity);
        }

        public async Task<Category> FindCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category?? throw new NullReferenceException("The category not found");
        }

        public override Task<IEnumerable<Category>> GetAll()
        {
            return base.GetAll();
        }
        public override async Task<bool> Update(Category entity)
        {
            var exsestingCategory = await _context.Categories.Where(c => c.Id == entity.Id).FirstOrDefaultAsync();
            if (exsestingCategory != null)
            {
                exsestingCategory.Name = entity.Name;
                exsestingCategory.Description = entity.Description;
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
