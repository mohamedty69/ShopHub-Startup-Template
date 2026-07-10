using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using myshop.BLL.DTOs.User;
using myshop.BLL.IServices;
using myshop.Entities.Models;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace myshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        #region Register
        public IActionResult Register(RegisterDTO regiser)
        {
            return View(regiser);
        }
        [HttpPost]
        public async Task<IActionResult> Registeration(RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                var check = await _userService.RegisterAsync(register);
                if (check.Succeeded)
                    return View("Login");
                else
                {
                    foreach (var error in check.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("RegisterUser", register);
        }
        #endregion
        [HttpPost]
        public async Task<IActionResult> LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO login)
        {
            var result =await _userService.LoginAsync(login);
            if (result.Succeeded)
            { 
                if ("Admin" == await _userService.GetRoleAsync(login))
                {
                    return RedirectToAction("Index", "Product");
                }
                return PartialView("_LoginPartial"); ;
            }
            else
                return Json("User is not logged in");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Role()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Roles(RolesDTO role)
        {
            var result =  await _userService.AddRolesAsync(role);
            if (result.Succeeded) return Json($"Role is added {role.RoleName}");
            else return Json($"Role is not added {role.RoleName}");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DisplayUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return View( users);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDTO editUser)
        {
            var result = await _userService.UpdateUserAsync(editUser);
            if (result.Succeeded) return RedirectToAction("DisplayUsers");
            else return Json(result.Errors.FirstOrDefault());

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Succeeded) return RedirectToAction("DisplayUsers");
            else return Json($"User is already deleted");
        }
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOut();
            return RedirectToAction("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}