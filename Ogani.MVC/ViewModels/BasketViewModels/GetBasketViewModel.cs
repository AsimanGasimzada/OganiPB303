namespace Ogani.MVC.ViewModels;

public class GetBasketViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; }=null!;
    public decimal Price { get; set; }
    public int Count { get; set; }
}
