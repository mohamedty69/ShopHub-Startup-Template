using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Product.Customer;
using myshop.BLL.IServices;
using System.Text.Json;

namespace myshop.PL.Controllers
{
    // make the controller inherit from Controller class in MVC
    // make the controller inherit from ControllerBase class in API
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public CustomerController(IUserService userService, IProductService productService,
            ICartService cartService)
        {
            _userService= userService;
            _productService = productService;
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> DisplayProducts()
        {
            return View();
        }
        public async Task<IActionResult> DisplayProduct()
        {
            var listOfProducts = await _productService.GetAllProductsForCustomersAsync();
            return Json(listOfProducts);
        }
        public async Task<IActionResult> AddItemToCart(int id)
        {
            var result = await _cartService.AddProductToCartAsync(id);
            if (result)
                return RedirectToAction("DisplayProducts");
            return Json("Can not add item to cart");
        }
        public IActionResult DisplayItem()
        {
            var items = _cartService.GetCartItems();
                
            return View(items);
        }
        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await _cartService.RemoveItemFromCartAsync(id);
            if (result)
                return RedirectToAction("DisplayItem");
            return Json("The item can not be found");
        }
        public IActionResult IncreaseItem(int id)
        {
            var result = _cartService.IncreaseQuantityOfItemAsync(id);
            if (result)
                return RedirectToAction("DisplayItem");
            return Json("Failed to increase item");
        }
        public async Task<IActionResult> DecreaseItem(int id)
        {
            var result = await _cartService.DecreaseQuantityOfItemAsync(id);
            if (result)
                return RedirectToAction("DisplayItem");
            return Json("Failed to decrease item");
        }
        public IActionResult DeleteCustomerCart()
        {
            var result = _cartService.DeleteCart();
            return RedirectToAction("DisplayProducts");
        }
    }
}
