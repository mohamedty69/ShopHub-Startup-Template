using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.DAL.IRepository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Repository
{
    public class UserRepo : GenericRepo<ApplicationUser>, IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ApplicationUser> _dbSet;
        public UserRepo(ApplicationDbContext context): base(context)
        {
      
        }
        public override Task<IEnumerable<ApplicationUser>> GetAll()
        {
                        return base.GetAll();
        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await dbSet.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new NullReferenceException("User not found");
            return user;
        }
        
    }
}
