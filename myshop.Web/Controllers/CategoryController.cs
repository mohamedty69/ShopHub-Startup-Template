using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Category;
using myshop.BLL.IServices;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var listOfCategory = await _categoryService.GetCategoriesAsync();
            return View(listOfCategory);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO categorydto)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreaetCategoryAsync(categorydto);
                return RedirectToAction("Index");
            }
            return View(categorydto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                NotFound();
            }
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(categoryDTO);
                return RedirectToAction("Index");
            }
            return View(categoryDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var cat = await _categoryService.GetCategoryByIdAsync(id);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryIndb = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryIndb == null)
            {
                NotFound();
            }
            await _categoryService.DeleteCategoryAsync(id);
            TempData["Delete"] = "Item has Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
