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
        public Task CompleteTask();
    }
}
