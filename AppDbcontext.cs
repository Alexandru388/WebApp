using WebApplication1.Models;

namespace WebApplication1
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<Universitate> Universitati { get; set; }
        public DbSet<Camin> Camine { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Cazare> Cazari { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Log> Loguri { get; set; }
        
        // Add a DbSet for CazareInfo
        public DbSet<CazareInfo> CazareInfos { get; set; } // Required to query CazareInfo

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure CazareInfo as a keyless entity
            modelBuilder.Entity<CazareInfo>().HasNoKey();
        }
    }

}