using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interface.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<Guid> InsertAsync(T entity);
    System.Threading.Tasks.Task SaveChangesAsync();
}
