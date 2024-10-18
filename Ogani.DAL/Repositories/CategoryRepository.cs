using Microsoft.EntityFrameworkCore;
using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;
using System.Linq.Expressions;

namespace Ogani.DAL.Repositories;

public class CategoryRepository :  EfCoreRepository<Category> , ICategoryRepository
{

    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

}
