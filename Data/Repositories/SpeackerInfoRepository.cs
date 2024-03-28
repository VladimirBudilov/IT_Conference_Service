using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data.Repositories
{
    public class SpeackerInfoRepository : ISpeackerInfoRepository
    {
        private readonly DbSet<SpeakerInfo> _speakerInfo;

        public SpeackerInfoRepository(ConferenceDbContext context)
        {
            _speakerInfo = context.SpeackerInfo;
        }

        public async Task<IEnumerable<SpeakerInfo>> GetAllAsync()
        {
            return await _speakerInfo.ToListAsync();
        }

        public async Task<SpeakerInfo> GetByIdAsync(Guid id)
        {
            return await _speakerInfo.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SpeakerInfo> AddAsync(SpeakerInfo speackerInfo)
        {
            await _speakerInfo.AddAsync(speackerInfo);
            return speackerInfo;
        }

        public async Task<SpeakerInfo> UpdateAsync(SpeakerInfo speackerInfo)
        {
            _speakerInfo.Update(speackerInfo);
            return speackerInfo;
        }

        public async Task<SpeakerInfo> DeleteAsync(Guid id)
        {
            var speackerInfo = await _speakerInfo.FirstOrDefaultAsync(x => x.Id == id);
            if (speackerInfo == null)
            {
                return null;
            }

            _speakerInfo.Remove(speackerInfo);
            return speackerInfo;
        }

    }
}
