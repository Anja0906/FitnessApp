using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FitnessApp.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FitnessAppContext>
    {
        public FitnessAppContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath("C:/Users/anjap/OneDrive/Radna površina/FitnessApp/FitnessApp")
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FitnessAppContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new FitnessAppContext(optionsBuilder.Options);
        }
    }
}
