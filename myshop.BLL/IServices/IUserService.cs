using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs.Product.Admin;
using myshop.BLL.DTOs.User;
using myshop.BLL.Mapping.UserMapping.OutgoingData;
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
        public Task<IEnumerable<DisplayUserDTO>> GetAllUsersAsync();
        public Task<EditUserDTO> GetUserByIdAsync(string id);
        public Task<IdentityResult> DeleteUserAsync(string id);
        public Task<IdentityResult> UpdateUserAsync(EditUserDTO editUserDTO);
        public Task LogOut();

        public Task<IEnumerable<DisplayUserDTO>> GetAllDeletedUsers();
        public Task<bool> RestoreUserAsync(string userId);

    }
}
