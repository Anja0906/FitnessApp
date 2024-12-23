using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace FitnessApp.Infrastructure.Context
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using FitnessAppContext dbContext =
                scope.ServiceProvider.GetRequiredService<FitnessAppContext>();

            dbContext.Database.Migrate();
            DbInitializer.Seed(dbContext);
        }
    }
}
