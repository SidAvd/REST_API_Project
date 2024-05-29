using Microsoft.EntityFrameworkCore;
using REST_API_Project.Models;

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
            // Creating many-to-many relationship, Join Table WorkerErrand
            /*modelBuilder.Entity<Worker>().HasMany(x => x.Errands).WithMany(x => x.Workers).UsingEntity(j => j.ToTable("WorkerErrand"));*/

            // Database and Tables configuration
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(worker => worker.Id);
                entity.Property(worker => worker.Name).IsRequired();
            });

            modelBuilder.Entity<Errand>(entity =>
            {
                entity.HasKey(errand => errand.Id);
                entity.Property(errand => errand.Name).IsRequired();
                entity.Property(errand => errand.IsCompleted).IsRequired();
            });

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
    }
}
