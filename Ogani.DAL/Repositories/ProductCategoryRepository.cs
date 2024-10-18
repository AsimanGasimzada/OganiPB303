using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;

namespace Ogani.DAL.Repositories;

public class ProductCategoryRepository : EfCoreRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context)
    {
    }
}
