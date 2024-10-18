using Microsoft.AspNetCore.Http;

namespace Ogani.BLL.ViewModels.CategoryViewModels;

public class CategoryViewModel : IViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}

public class CategoryCreateViewModel : IViewModel
{
    public required string Name { get; set; }
    public required IFormFile ImageFile { get; set; }
    public string? ImageUrl { get; set; }
}

public class CategoryUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ImageUrl { get; set; }
}