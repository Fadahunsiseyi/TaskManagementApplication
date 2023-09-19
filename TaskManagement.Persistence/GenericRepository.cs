using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Domain.Entities;
using System.Linq.Expressions;

namespace TaskManagement.Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private ApplicationDbContext ApplicationDbContext { get; }
    private DbSet<T> DbSet { get; }
    public GenericRepository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = applicationDbContext.Set<T>();
    }
   public async Task<Guid> InsertAsync(T entity)
    {
        entity.Created = DateTime.UtcNow;
        await DbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task<IEnumerable<T>> GetAllAsync(int? skip, int? take)
    {
        return await DbSet.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        query = query.Where(x => x.Id == id);
        foreach (var include in includes) query = query.Include(include);

        return await query.SingleOrDefaultAsync();
    }
    public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        foreach (var filter in filters) query = query.Where(filter);
        foreach (var include in includes) query = query.Include(include);
        return await query.ToListAsync();
    }

    
    public async System.Threading.Tasks.Task SaveChangesAsync()
    {
        await ApplicationDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        entity.Created = DateTime.UtcNow;
        DbSet.Attach(entity);
        ApplicationDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        if (ApplicationDbContext.Entry(entity).State == EntityState.Detached)
            DbSet.Attach(entity);
        DbSet.Remove(entity);
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.AnyAsync(x => x.Id == id);
    }

}
