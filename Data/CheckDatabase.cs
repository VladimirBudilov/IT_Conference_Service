using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data
{
    public static class CheckDatabase
    {
        public static void EnsureExist(IApplicationBuilder app)
        {
            ConferenceDbContext context = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<ConferenceDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            context.Database.EnsureCreated();

        }

    }
}
