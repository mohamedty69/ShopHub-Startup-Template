using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO);
        public Task<IdentityResult> AddRolesAsync(RolesDTO roleName);
        public Task<SignInResult> LoginAsync(LoginDTO log);
        public Task<string> GetRoleAsync(LoginDTO log);
    }
}
