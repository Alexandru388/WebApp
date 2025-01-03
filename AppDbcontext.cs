using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1
{

    public class AppDbContext : DbContext
    {
        public DbSet<Universitate> Universitati { get; set; }
        public DbSet<Camin> Camine { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Cazare> Cazari { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Log> Loguri { get; set; }
        
        
        public DbSet<CazareInfo> CazareInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CazareInfo>().HasNoKey();
            modelBuilder.Entity<Models.Camin>().HasNoKey();
        }
        
        
    }
}