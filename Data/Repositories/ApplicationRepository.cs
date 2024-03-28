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
            return await _dbSet.AsNoTracking().Where(x => x.AuthorInfoId == id)
                         .Include(x => x.AuthorInfo)
                         .FirstOrDefaultAsync();
        }

        public async Task<Application> GetByIdWithDetaiksAsync(Guid id)
        {
            return await _dbSet.Where(x => x.AuthorInfoId == id)
                         .Include(x => x.AuthorInfo)
                         .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Application>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(x => x.AuthorInfo).ToListAsync();
        }
    }
}
