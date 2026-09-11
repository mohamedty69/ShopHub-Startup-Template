using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Order;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using System.Security.Claims;
using System.Runtime.CompilerServices;
using Stripe.Tax;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Identity;
using myshop.Entities.Models;

namespace myshop.PL.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderServices _orderService;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckOutController(UserManager<ApplicationUser> userManager, IEmailService emailService,ICartService cartService, IOrderServices orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _emailService = emailService;
            _userManager = userManager; 
        }
        [HttpGet]
        public IActionResult CheckOut()
        {
            var cartItems = _cartService.GetCartItems();
            ViewBag.CartItems = cartItems;
            return View(new CheckOutOrderDTO());
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckOutOrderDTO order)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? throw new NullReferenceException("user can not be found");
            var user = await _userManager.FindByIdAsync(userId)?? throw new NullReferenceException("user can not be found");
            if (!ModelState.IsValid)
            { 
                return RedirectToAction("CheckOut");
            }
            var Id = await _orderService.PlaceOrderAsync(userId, order);
            if (Id != 0)
            {
                await Task.Delay(2000);
                TempData["Success"] = "Your order has been placed successfully!";
                var orderSummary = await _orderService.GetUserOrdersAsync(Id);
                await _emailService.SendOrderConfirmationEmailAsync(user.Email, user.UserName,orderSummary);
                return RedirectToAction("DisplayProducts", "Customer");
            }
            TempData["Error"] = "Something went wrong while placing your order.";
            return RedirectToAction("CheckOut");
        }
    }
}
