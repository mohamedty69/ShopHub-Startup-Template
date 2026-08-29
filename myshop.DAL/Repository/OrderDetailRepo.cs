using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Repository
{
    public class OrderDetailRepo : GenericRepo<OrderDetail> , IOrderDetailRepo
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
