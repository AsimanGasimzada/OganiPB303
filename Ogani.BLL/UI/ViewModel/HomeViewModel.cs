using Ogani.BLL.ViewModels.CategoryViewModels;
using Ogani.BLL.ViewModels.ProductViewModels;

namespace Ogani.BLL.UI.ViewModel;

public class HomeViewModel
{
    public List<ProductViewModel>? Products { get; set; }
    public List<CategoryViewModel>? Categories { get; set; }
}
