using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myshop.BLL.DTOs.Product;
using myshop.BLL.IServices;
using myshop.DataAccess;
using myshop.Entities.Models;
using myshop.Entities.ViewModels;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ICategoryService categoryService,IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var listOfProducts = await _productService.GetAllProductsAsync();
            return Json(new { data = listOfProducts });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var product = new CreateProductDTO();
            var cat= await _categoryService.GetCategoriesAsync();
            product.categories = cat.ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO productDTO, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productDTO.Img = @"Images\Products\" + filename + ext;
                }
                var check = await _productService.AddProductAsync(productDTO);
                if (check)
                { 
                    TempData["Create"] = "Product has been Created Successfully";
                    return RedirectToAction("Index");
                }
                return View(productDTO);
            }
            return View(productDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id.Value);
            var categories = await _categoryService.GetCategoriesAsync();
            product.categories = categories.ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductDTO editProductDTO, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    if (editProductDTO.Img != null)
                    {
                        var oldimg = Path.Combine(RootPath, editProductDTO.Img.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    editProductDTO.Img = @"Images\Products\" + filename + ext;
                }
                var result = await _productService.UpdateProductAsync(editProductDTO);
                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(editProductDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                var check = await _productService.DeleteProductAsync(id);
                if (check)
                { 
                    var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, product.Img.TrimStart('\\'));
                    if (System.IO.File.Exists(oldimg))
                    {
                        System.IO.File.Delete(oldimg);
                    }
                    return Json(new { success = true, message = "file has been Deleted" });
                }
            }
            return Json(new { success = false, message = "Error while Deleting" });
        }


    }
}
