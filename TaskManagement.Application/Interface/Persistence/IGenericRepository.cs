using System.Linq.Expressions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interface.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Guid> InsertAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(int? skip, int? take);
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(Guid id);
    System.Threading.Tasks.Task SaveChangesAsync();
}
