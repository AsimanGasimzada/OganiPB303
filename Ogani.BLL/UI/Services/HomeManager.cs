using Microsoft.EntityFrameworkCore;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.UI.Services.Contracts;
using Ogani.BLL.UI.ViewModel;

namespace Ogani.BLL.UI.Services;

public class HomeManager : IHomeService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeManager(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<HomeViewModel> GetHomeViewModelAsync()
    {
       var categories = await _categoryService.GetAllAsync();
       var products = await _productService.GetAllAsync(include:x=>x.Include(y=>y.ProductImages!).Include(c=>c.ProductCategories)!.ThenInclude(t=>t.Category)!);
        var model = new HomeViewModel
        {
            Categories = categories,
            Products = products
        };

        return model;
    }
}
