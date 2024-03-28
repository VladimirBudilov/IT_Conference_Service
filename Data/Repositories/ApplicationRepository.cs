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

        public async Task<Application> GetFullDataByIdAsNoTrackingAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.AuthorInfoId == id)
                         .Include(x => x.AuthorInfo)
                         .FirstOrDefaultAsync();
        }

        public async Task<Application> GetFullDataByIdAsync(Guid id)
        {
            return await _dbSet.Where(x => x.AuthorInfoId == id)
                         .Include(x => x.AuthorInfo)
                         .FirstOrDefaultAsync();
        }
    }
}
