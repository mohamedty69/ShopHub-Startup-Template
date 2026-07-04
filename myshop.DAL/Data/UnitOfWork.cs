using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.DAL.Iconfiguration;
using myshop.DAL.IRepository;
using myshop.DAL.Repository;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IUserRepo Users { get; private set; }

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context
            )
        {
            _context = context;
            Users = new UserRepo(_context);
        }
        public async Task CompleteTask()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
