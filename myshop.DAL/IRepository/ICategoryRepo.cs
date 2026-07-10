using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        public Task<Category> FindCategoryByIdAsync(int id);
    }
}
