using FitnessApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Infrastructure.Context
{
    public class FitnessAppContext : DbContext
    {
        public FitnessAppContext(DbContextOptions<FitnessAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(w => w.Id).ValueGeneratedOnAdd();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.HashedPassword).IsRequired();
                entity.HasMany(u => u.Workouts)
                      .WithOne(w => w.User)
                      .HasForeignKey(w => w.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Id).ValueGeneratedOnAdd();
                entity.Property(w => w.Duration).IsRequired();
                entity.Property(w => w.DateTime).IsRequired();
                entity.HasOne(w => w.ExerciseType)
                      .WithMany()
                      .HasForeignKey(w => w.ExerciseTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ExerciseType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(w => w.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(250);
            });
        }
    }
}