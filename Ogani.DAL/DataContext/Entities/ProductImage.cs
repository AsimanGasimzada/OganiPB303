namespace Ogani.DAL.DataContext.Entities;

public class ProductImage : BaseEntity
{
    public required string ImageUrl { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}

