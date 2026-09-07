using myshop.DAL.IRepository;
using myshop.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Iconfiguration
{
    public interface IUnitOfWork
    {
        public IUserRepo Users { get; }
        public ICategoryRepo Categories { get; }
        public IProductRepo Products { get; }   
        public IOrderHeaderRepo orderHeaderRepo { get; }
        public IOrderDetailRepo orderDetailRepo { get; }
        public IReviewRepo reviewRepo { get; }
        public Task CompleteTask();
    }
}
