using AutoMapper;
using Microsoft.AspNetCore.Identity;
using myshop.BLL.DTOs;
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
        private readonly UserManager<ApplicationUser>  _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserServices(IMapper mapper, IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {
            var mappingUser = _mapper.Map<ApplicationUser>(registerDTO);
            await _userManager.CreateAsync(mappingUser, registerDTO.Password);
            return await _userManager.AddToRoleAsync(mappingUser, "Customer");
        }
        public async Task<IdentityResult> AddRolesAsync (RolesDTO roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName.RoleName));
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO log)
        {
            try
            {
                var userexist = await _unitOfWork.Users.GetUserByEmail(log.Email);
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
            var user = await _unitOfWork.Users.GetUserByEmail(log.Email);
            var role = await _userManager.GetRolesAsync(user);
            return role.FirstOrDefault()?? throw new NullReferenceException("The user has no role assigned.") ;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.GetUsersInRoleAsync("Customer");
        }
    }
}
