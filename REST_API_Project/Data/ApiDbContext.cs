using Microsoft.EntityFrameworkCore;
using REST_API_Project.Models;

namespace REST_API_Project.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Worker> Workers { get; set; }
        public DbSet<Errand> Errands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating many-to-many relationship, Join Table WorkerErrand
            modelBuilder.Entity<Worker>().HasMany(x => x.Errands).WithMany(x => x.Workers).UsingEntity(j => j.ToTable("WorkerErrand"));
        }
    }
}
