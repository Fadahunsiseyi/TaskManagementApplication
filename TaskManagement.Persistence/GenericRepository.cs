using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interface.Persistence;
using TaskManagement.Domain.Entities;

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
    public async Task<T?> GetByIdAsync(Guid id)
    {
        IQueryable<T> query = DbSet;
        query = query.Where(x => x.Id == id);
        var result = await query.FirstOrDefaultAsync();
        return result;
        //return await query.FirstOrDefaultAsync();
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

}
