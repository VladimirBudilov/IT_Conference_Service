using IT_Conference_Speaker__Service.Data.Entitiess;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Speaker__Service.Data
{
    public class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options) : base(options)
        {
        }

        public DbSet<Speaker> Speakers { get; set; }

    }
}
