namespace WebApplication1
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<Universitate> Universitati { get; set; }
        public DbSet<Camin> Camine { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Student> Studentii { get; set; }
        public DbSet<Cazare> Cazari { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Log> Logs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}