using Microsoft.EntityFrameworkCore.Query;
using Ogani.BLL.ViewModels;
using Ogani.DAL.DataContext.Entities;
using System.Linq.Expressions;

namespace Ogani.BLL.Services.Contracts;

public interface ICrudService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
    where TEntity : BaseEntity
    where TViewModel : IViewModel
    where TCreateViewModel : IViewModel
    where TUpdateViewModel : IViewModel
{
    Task<TViewModel?> GetAsync(int id);
    Task<TViewModel?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<List<TViewModel>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<TViewModel> CreateAsync(TCreateViewModel entity);
    Task<TViewModel> UpdateAsync(TUpdateViewModel entity);
    Task<TViewModel> DeleteAsync(int id);
}
