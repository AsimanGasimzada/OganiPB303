namespace Ogani.DAL.DataContext.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public List<ProductCategory>? ProductCategories { get; set; }
    public List<ProductImage>? ProductImages { get; set; }
}

