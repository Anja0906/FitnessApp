using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace FitnessApp.Infrastructure
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using FitnessAppContext dbContext =
                scope.ServiceProvider.GetRequiredService<FitnessAppContext>();

            dbContext.Database.Migrate();
            //dbContext.SeedData(dbContext);
        }
    }
}
