using IT_Conference_Service.Data.Entitiess;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data
{
    public class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.ApplicationInfo)
                .WithOne(b => b.Application)
                .HasForeignKey<Application>(b => b.ApplicationInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationInfo> ApplicationInfo { get; set; }
    }
}
