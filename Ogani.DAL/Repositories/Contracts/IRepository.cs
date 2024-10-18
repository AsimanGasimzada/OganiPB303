using Microsoft.EntityFrameworkCore.Query;
using Ogani.DAL.DataContext.Entities;
using System;
using System.Linq.Expressions;

namespace Ogani.DAL.Repositories.Contracts;

public interface IRepository<T> : IQuery<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include=null,Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy=null,bool IsTracking=true);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,bool ignoreQueryFilter=false);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
