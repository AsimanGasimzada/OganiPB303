using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ogani.BLL.ViewModels.CategoryViewModels;
using Ogani.BLL.ViewModels.ProductImageViewModels;

namespace Ogani.BLL.ViewModels.ProductViewModels;

public class ProductViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public List<ProductImageViewModel>? ProductImages { get; set; }
    public List<CategoryViewModel>? ProductCategories { get; set; }
}

public class ProductCreateViewModel : IViewModel
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public List<IFormFile>? Images { get; set; }
    public List<SelectListItem>? Categories { get; set; }
    public List<int>? CategoryIds { get; set; }
}

public class ProductUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public List<IFormFile>? Images { get; set; }
    public List<SelectListItem>? Categories { get; set; }
    public List<int>? CategoryIds { get; set; }
}