using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        public Task<bool> Add(T entity);
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> Update(T entity);
        public Task<bool> Delete(int id);


    }
}
