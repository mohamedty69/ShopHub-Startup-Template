using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface IOrderHeaderRepo : IGenericRepo<OrderHeader>
    {
        public Task<IEnumerable<OrderHeader>> GetAllOrdersByUserIdAsync(string userId);
        public Task<OrderHeader> GetOrderByUserIdAsync(int orderId);
    }
}
