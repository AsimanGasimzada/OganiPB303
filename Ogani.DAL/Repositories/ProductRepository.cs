using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;

namespace Ogani.DAL.Repositories;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
