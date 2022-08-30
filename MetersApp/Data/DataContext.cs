using Microsoft.EntityFrameworkCore;

namespace MetersApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Indication> Indication { get; set; }
        public DbSet<Meter> Meter { get; set; }
        public DbSet<Resourse> Resourse { get; set; }
        public DbSet<Unit> Unit { get; set; }
    }
}
