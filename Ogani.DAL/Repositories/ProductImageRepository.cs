using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;

namespace Ogani.DAL.Repositories;

public class ProductImageRepository : EfCoreRepository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(AppDbContext context) : base(context)
    {
    }
}
