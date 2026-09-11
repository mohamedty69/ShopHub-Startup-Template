using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Repository
{
    public class OrderHeaderRepo : GenericRepo<OrderHeader> , IOrderHeaderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepo(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllOrdersByUserIdAsync(string userId)
        {
            var userOrder = await _context.OrderHeaders.Where(o => o.ApplicationUserId == userId)
                .Include(od => od.OrderDetails).ThenInclude(p => p.Product).ToListAsync();
            return userOrder ?? throw new NullReferenceException("Can not find the order");
        }

        public async Task<OrderHeader> GetOrderByUserIdAsync(int orderId)
        {
            var order = await _context.OrderHeaders.Where(o => o.Id == orderId).
                Include(o => o.OrderDetails).ThenInclude(p => p.Product).AsNoTracking().FirstOrDefaultAsync();
            return order ?? throw new NullReferenceException("Order can not be found");
        }
    }
}
