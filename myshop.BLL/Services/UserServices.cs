using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using myshop.BLL.DTOs.User;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.Services
{
    public class UserServices : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        public UserServices(IMapper mapper, IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager
            , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {
            var mappingUser = _mapper.Map<ApplicationUser>(registerDTO);
            await _userManager.CreateAsync(mappingUser, registerDTO.Password);
            return await _userManager.AddToRoleAsync(mappingUser, "Customer");
        }
        public async Task<IdentityResult> AddRolesAsync(RolesDTO roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName.RoleName));
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO log)
        {
            try
            {
                var userexist = await _userManager.FindByEmailAsync(log.Email) ?? throw new NullReferenceException("User Not Found");
                if (await _userManager.IsLockedOutAsync(userexist))
                    throw new Exception($"Your account is locked out. Please try again later after {await _userManager.GetLockoutEndDateAsync(userexist)}.");
                var result = await _signInManager.PasswordSignInAsync(userexist, log.Password, log.IsPersistent, false);
                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(userexist);
                    return result;
                }
                else
                {
                    await _userManager.AccessFailedAsync(userexist);
                    if (await _userManager.GetAccessFailedCountAsync(userexist) == 5)
                        await _userManager.SetLockoutEndDateAsync(userexist, DateTimeOffset.UtcNow.AddMinutes(5));
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetRoleAsync(LoginDTO log)
        {
            var user = await _userManager.FindByEmailAsync(log.Email) ?? throw new NullReferenceException("User Not Found");
            var role = await _userManager.GetRolesAsync(user);
            return role.FirstOrDefault() ?? throw new NullReferenceException("The user has no role assigned.");
        }
        public async Task<IEnumerable<DisplayUserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Customer");
            var displayUsere = _mapper.Map<IEnumerable<DisplayUserDTO>>(users);
            return displayUsere;
        }
        public async Task<EditUserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NullReferenceException("User Not Found");
            var mappedUser = _mapper.Map<EditUserDTO>(user);
            mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? throw new NullReferenceException("The user has no role assigned.");
            mappedUser.IsLocked = await _userManager.IsLockedOutAsync(user);
            return mappedUser;
        }
        public async Task<IdentityResult> UpdateUserAsync(EditUserDTO editUserDTO)
        {
            var user = await _userManager.FindByIdAsync(editUserDTO.UserID) ?? throw new NullReferenceException("User Not Found");
            var role = await _userManager.GetRolesAsync(user);
            if (user.Email == editUserDTO.Email)
            {
                _mapper.Map(editUserDTO,user);
                var result = await _userManager.UpdateAsync(user);
                if (editUserDTO.IsLocked) await LockUserAsync(editUserDTO.UserID);
                else await UnlockUserAsync(editUserDTO.UserID);
                if (role.FirstOrDefault() != editUserDTO.Role) await ChangeRoleAsync(editUserDTO);
                return result;
            }
            else
            {
                var checkEmail = await _userManager.FindByEmailAsync(editUserDTO.Email);
                if (checkEmail == null)
                {
                    _mapper.Map(editUserDTO, user);
                    var result = await _userManager.UpdateAsync(user);
                    if (editUserDTO.IsLocked) await LockUserAsync(editUserDTO.UserID);
                    else await UnlockUserAsync(editUserDTO.UserID);
                    if (role.FirstOrDefault() != editUserDTO.Role) await ChangeRoleAsync(editUserDTO);
                    return result;
                }
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists." });
            }
        }
        public async Task<IdentityResult> DeleteUserAsync(string id) 
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NullReferenceException("User Not Found");

            var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == id)
            {
                return IdentityResult.Failed(new IdentityError { Description = "You cannot delete your own account." });
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                if (adminUsers.Count <= 1)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "You cannot delete the last remaining Admin account." });
                }
            }

            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> ChangeRoleAsync(EditUserDTO editUserDTO)
        {
            if (await _roleManager.RoleExistsAsync(editUserDTO.Role))
            {
                var user = await _userManager.FindByIdAsync(editUserDTO.UserID) ?? throw new NullReferenceException("User Not Found");
                var removeRole = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                if (removeRole.Succeeded)
                {
                    return await _userManager.AddToRoleAsync(user, editUserDTO.Role);
                }
                return IdentityResult.Failed(new IdentityError { Description = "Failed to remove existing roles." });
            }
            else 
                return IdentityResult.Failed(new IdentityError { Description = "The role does not exist" });
        }
        public async Task<IdentityResult> LockUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NullReferenceException("User Not Found");
            return await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }
        public async Task<IdentityResult> UnlockUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NullReferenceException("User Not Found");
            return await _userManager.SetLockoutEndDateAsync(user, null);
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
