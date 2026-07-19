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
        public CustomerController(IUserService userService, IProductService productService)
        {
            _userService= userService;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> DisplayProducts()
        {
            var listOfProducts = await _productService.GetAllProductsForCustomersAsync();
            return View(listOfProducts);
        }
        public async Task<IActionResult> AddItemToCart(int id)
        {
            var result = await _productService.AddProductToCartAsync(id);
            if (result)
                return RedirectToAction("DisplayProducts");
            return Json("Can not add item to cart");
        }
        public IActionResult DisplayItem()
        {
            var items = _productService.GetCartItems();
            return View(items);
        }
    }
}
