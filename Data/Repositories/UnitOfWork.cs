using IT_Conference_Service.Data.Repositories.Interfaces;

namespace IT_Conference_Service.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConferenceDbContext _context;

        public UnitOfWork(ConferenceDbContext context,
            IAuthorInfoRepository speackerInfoRepository
            , IApplicationRepository applicationRepository
            )
        {
            _context = context;
            SpeackerInfoRepository = speackerInfoRepository;
            ApplicationRepository = applicationRepository;
        }

        public IAuthorInfoRepository SpeackerInfoRepository { get; private set; }
        public IApplicationRepository ApplicationRepository { get; private set; }
  

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
