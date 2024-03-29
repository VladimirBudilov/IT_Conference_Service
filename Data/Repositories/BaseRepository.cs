using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Validation;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEnity
    {
        protected readonly ConferenceDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ConferenceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<TEntity> GetByIdAsyncAsNoTracking(Guid id)
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
