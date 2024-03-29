using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ConferenceDbContext context) : base(context)
        {
        }

        public async Task<Application> GetByIdWithDetailsAsNoTrackingAsync(Guid id)
        {

            var entity = await _dbSet.AsNoTracking().Where(x => x.Id == id)
                         .Include(x => x.ApplicationInfo)
                         .FirstOrDefaultAsync();
            return entity;
        }

        public async Task<Application> GetByIdWithDetaiksAsync(Guid id)
        {
            var entity = await _dbSet.Where(x => x.Id == id)
                         .Include(x => x.ApplicationInfo)
                         .FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IEnumerable<Application>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(x => x.ApplicationInfo).ToListAsync();
        }

        public async Task<Application> GetDraft(Guid id)
        {
            var entity = await _dbSet.AsNoTracking().Where(x => x.AuthorId == id && x.IsSent == false)
                         .Include(x => x.ApplicationInfo)
                         .FirstOrDefaultAsync();
            return entity;
        }
    }
}
