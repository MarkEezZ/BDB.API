namespace BDB.API.Repositories
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DetectorsConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
        }

        public DbSet<Fine> Fines { get; set; }
        public DbSet<Detector> Detectors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Gratitude> Gratitudes { get; set; }
    }
}
