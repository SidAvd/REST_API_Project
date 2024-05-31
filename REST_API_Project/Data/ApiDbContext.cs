﻿using Microsoft.EntityFrameworkCore;
using REST_API_Project.Models;
using System.Security.Claims;

namespace REST_API_Project.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandWorker> ErrandWorkers { get; set; } // Join Table, many-to-many

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Database and Tables configuration
            // Worker Table
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(worker => worker.Id);
                entity.Property(worker => worker.Name).IsRequired();
                entity.Property(worker => worker.Age).HasPrecision(3, 0);
            });
            // Errand Table
            modelBuilder.Entity<Errand>(entity =>
            {
                entity.HasKey(errand => errand.Id);
                entity.Property(errand => errand.Name).IsRequired();
                entity.Property(errand => errand.IsCompleted).IsRequired();
                entity.Property(errand => errand.DifficultyLevel).HasPrecision(1, 0);
            });
            // Join Table ErrandWorker, many-to-many relationship
            modelBuilder.Entity<ErrandWorker>(entity =>
            {
                entity.HasKey(errandWorker => new { errandWorker.WorkerId, errandWorker.ErrandId });

                entity.HasOne(errandWorker => errandWorker.Errand)
                    .WithMany(errand => errand.ErrandWorkers)
                    .HasForeignKey(errandWorker => errandWorker.ErrandId)   // Foreign key, join table
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(errandWorker => errandWorker.Worker)
                    .WithMany(worker => worker.ErrandWorkers)
                    .HasForeignKey(errandWorker => errandWorker.WorkerId)   // Foreign key, join table
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
        public DbSet<REST_API_Project.Models.WorkerDTO> WorkerDTO { get; set; } = default!;
    }
}
