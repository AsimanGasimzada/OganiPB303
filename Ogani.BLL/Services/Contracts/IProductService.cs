using Ogani.BLL.ViewModels.ProductViewModels;
using Ogani.DAL.DataContext.Entities;

namespace Ogani.BLL.Services.Contracts
{
    public interface IProductService : ICrudService<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>
    {
    }
}
