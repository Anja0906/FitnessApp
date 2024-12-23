using FitnessApp.Infrastructure.Context;
using FitnessApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Tests.Infrastructure
{
    public class WorkoutRepositoryTestSetup
    {
        public TestFitnessAppContext DataContext { get; private set; }
        public WorkoutRepository WorkoutRepository { get; private set; }

        public void InitializeDataContext(string testName)
        {
            var dbContextOptions = new DbContextOptionsBuilder<FitnessAppContext>()
                .UseInMemoryDatabase(databaseName: $"{testName}_{Guid.NewGuid()}") // Ensure unique DB name per test
                .Options;

            DataContext = new TestFitnessAppContext(dbContextOptions);
            DataContext.Database.EnsureDeleted();
            DataContext.Database.EnsureCreated(); 

            WorkoutRepository = new WorkoutRepository(DataContext);
        }
    }
}
