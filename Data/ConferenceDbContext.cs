using IT_Conference_Service.Data.Entitiess;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data
{
    public class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<SpeakerInfo> SpeackerInfo { get; set; }


    }
}
