using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using myshop.BLL.DTOs;
using myshop.BLL.IServices;
using myshop.DAL.Iconfiguration;
using myshop.Entities.Models;

namespace myshop.PL.Controllers
{
    // make the controller inherit from Controller class in MVC
    // make the controller inherit from ControllerBase class in API
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService= userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
