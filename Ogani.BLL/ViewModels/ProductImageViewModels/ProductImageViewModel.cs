using Microsoft.AspNetCore.Http;

namespace Ogani.BLL.ViewModels.ProductImageViewModels;

public class ProductImageViewModel : IViewModel
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProductImageCreateViewModel : IViewModel
{
    public required IFormFile Image {  get; set; }
    public string? ImageUrl { get; set; }
}
public class ProductImageUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required IFormFile Image { get; set; }
    public string? ImageUrl { get; set; }
}
