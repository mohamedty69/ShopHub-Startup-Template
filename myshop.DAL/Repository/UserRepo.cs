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
        public UserRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
