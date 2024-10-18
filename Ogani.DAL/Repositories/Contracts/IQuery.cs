using Ogani.DAL.DataContext.Entities;

namespace Ogani.DAL.Repositories.Contracts;

public interface IQuery<T> where T : BaseEntity
{
    IQueryable<T> Query();
}
