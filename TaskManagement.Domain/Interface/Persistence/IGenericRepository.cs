using System.Linq.Expressions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interface.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Guid> InsertAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(int? skip, int? take);
    Task<T?> GetByIdAsync(Guid id);
    System.Threading.Tasks.Task SaveChangesAsync();
}
