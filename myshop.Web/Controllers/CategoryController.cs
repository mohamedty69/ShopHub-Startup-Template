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

        //[HttpPost]
        //public IActionResult Edit(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Categories.Update(category);

        //        _context.SaveChanges();
        //        TempData["Update"] = "Data has Updated Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null | id == 0)
        //    {
        //        NotFound();
        //    }
        //    var categoryIndb = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

        //    return View(categoryIndb);
        //}

        //[HttpPost]
        //public IActionResult DeleteCategory(int? id)
        //{
        //    var categoryIndb = _context.Categories.FirstOrDefault(x => x.Id == id);
        //    if (categoryIndb == null)
        //    {
        //        NotFound();
        //    }
        //    _context.Categories.Remove(categoryIndb);
        //    _context.SaveChanges();
        //    TempData["Delete"] = "Item has Deleted Successfully";
        //    return RedirectToAction("Index");
        //}

    }
}
