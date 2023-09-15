using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interface.Persistence;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistence
{
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

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await ApplicationDbContext.SaveChangesAsync();
        }
    }
}
