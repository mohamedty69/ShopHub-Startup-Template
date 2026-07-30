using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        public Task<Product> GetProductByIdAsync(int id);
        public Task<List<Product>> GetProductWithPagination(string searchWord, string sortColumn,string sortOrder ,int page, int pageSize);
        public Expression<Func<Product,object>> GetSortName(string sortColumn);
    }
}
