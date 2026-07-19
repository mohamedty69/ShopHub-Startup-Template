using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Product.Admin;
using myshop.BLL.IServices;

namespace myshop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ICategoryService categoryService,
            IProductService productService,
            IFileService fileService,
            IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _fileService = fileService;
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
                var imagePath = await _fileService.SaveFileAsync(file, RootPath);
                try { 
                    productDTO.Img = imagePath;
                    var check = await _productService.AddProductAsync(productDTO);
                    if (check)
                    { 
                        TempData["Create"] = "Product has been Created Successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch 
                { 
                    return View(productDTO);
                }
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
                    if (editProductDTO.Img != null)
                    {
                        var oldimg = Path.Combine(RootPath, editProductDTO.Img.TrimStart('\\'));
                        _fileService.DeleteFile(oldimg);
                    }
                    var imagePath = await _fileService.SaveFileAsync(file, RootPath);
                    editProductDTO.Img = imagePath;
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
                    var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, product.Img);
                    _fileService.DeleteFile(oldimg);
                    return Json(new { success = true, message = "file has been Deleted" });
                }
            }
            return Json(new { success = false, message = "Error while Deleting" });
        }


    }
}
