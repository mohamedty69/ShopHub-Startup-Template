using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Order;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using System.Security.Claims;
using System.Runtime.CompilerServices;
using Stripe.Tax;
using System.Net.WebSockets;

namespace myshop.PL.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderServices _orderService;
        public CheckOutController(ICartService cartService, IOrderServices orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            { 
                return RedirectToAction("CheckOut");
            }
            var check = await _orderService.PlaceOrderAsync(userId, order);
            if (check)
            {
                await Task.Delay(2000);
                TempData["Success"] = "Your order has been placed successfully!";
                return RedirectToAction("DisplayProducts", "Customer");
            }
            TempData["Error"] = "Something went wrong while placing your order.";
            return RedirectToAction("CheckOut");
        }
    }
}
