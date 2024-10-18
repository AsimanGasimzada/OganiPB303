using AutoMapper;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.ViewModels.ProductViewModels;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;

namespace Ogani.BLL.Services
{
    public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
    {
        public ProductManager(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
