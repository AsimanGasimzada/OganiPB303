using Microsoft.AspNetCore.Mvc;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.ViewModels.CategoryViewModels;

namespace Ogani.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _categoryService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var vm=await _categoryService.GetUpdatedCategoryAsync(id);

            return View(vm);    
        }

        [HttpPost] 
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _categoryService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
