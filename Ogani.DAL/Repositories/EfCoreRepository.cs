using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories.Contracts;
using System.Linq.Expressions;

namespace Ogani.DAL.Repositories;

public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;



    public EfCoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool ignoreQueryFilter = false)
    {
        //var query = Query();
        //if (predicate != null)
        //    query = query.Where(predicate);
        //var result = await query.ToListAsync();

        //return result;
        IQueryable<T> query = _context.Set<T>();

        if (ignoreQueryFilter)
            query = query.IgnoreQueryFilters();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null)
            query = include(query);

        if (orderBy != null)
            query = orderBy(query);

        var result = await query.ToListAsync();

        return result;
    }


    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,bool IsTracking=true)
    {
        var query = Query();

        if (predicate != null)
            query = query.Where(predicate);

        if (!IsTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        if (orderBy != null)
            query = orderBy(query);

        var result = await query.FirstOrDefaultAsync();

        return result;
    }
    public virtual async Task<T?> GetAsync(int id)
    {
        var query = Query();
        var result = await query.FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        var entityEntry = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var entityEntry = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;

    }
    public virtual async Task<T> DeleteAsync(T entity)
    {
        var entityEntry = _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public IQueryable<T> Query()
    {
        return _context.Set<T>();
    }
}
