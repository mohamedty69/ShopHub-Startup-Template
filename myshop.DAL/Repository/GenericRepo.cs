using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var existuser = await dbSet.FindAsync(id);
            if (existuser == null)
                return false;
            dbSet.Remove(existuser);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            
            return await dbSet.ToListAsync();
        }
        
        public async Task<T> GetById(int id)
        {
            var listOfUsers = await dbSet.FindAsync(id);
            if (listOfUsers == null)
                 throw new ArgumentNullException("The list is empty");
            return listOfUsers;
        }

        public virtual async Task<bool> Update(T entity)
        {
            var existing = await dbSet.FindAsync(entity);
            if (existing != null)
            { 
                dbSet.Update(existing);
                return true;
            }
            return false;
        }
    }
}
