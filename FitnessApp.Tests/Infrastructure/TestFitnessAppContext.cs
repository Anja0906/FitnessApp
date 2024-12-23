using FitnessApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace FitnessApp.Tests.Infrastructure
{
    public class TestFitnessAppContext : FitnessAppContext
    {
        public TestFitnessAppContext(DbContextOptions<FitnessAppContext> options) : base(options) { }
    }
}
