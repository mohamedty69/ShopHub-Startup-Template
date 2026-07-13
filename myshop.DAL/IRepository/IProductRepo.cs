using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        public Task<Product> GetProductByIdAsync(int id);
    }
}
