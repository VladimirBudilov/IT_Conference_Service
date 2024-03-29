using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data.Repositories
{
    public class AuthorInfoRepository : BaseRepository<ApplicationInfo>, IAuthorInfoRepository
    {
        public AuthorInfoRepository(ConferenceDbContext context) : base(context)
        {
        }
    }
}
