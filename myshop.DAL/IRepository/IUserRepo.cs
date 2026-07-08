using Microsoft.AspNetCore.Identity;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.IRepository
{
    public interface IUserRepo : IGenericRepo<ApplicationUser>
    {
    }
}
