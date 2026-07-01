using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence
{
    public class SurfsUpDbContext : DbContext
    {
        public DbSet<MswSurfSpot> MswSurfSpots { get; set; }
        public DbSet<BafuSurfSpot> BafuSurfSpots { get; set; }

        public SurfsUpDbContext()
        {
        }

        public SurfsUpDbContext(DbContextOptions<SurfsUpDbContext> options) : base(options)
        {
        }

        // The default location for the Sqlite database file, used when no
        // DbContextOptions are supplied (e.g. by EF tooling or the parameterless ctor).
        public static string GetDefaultDbPath()
        {
            /*
             * Environment.SpecialFolder.LocalApplicationData:
             * Windows: User\AppData\Local
             * Linux: home/.local/share
             */
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return $"{path}{System.IO.Path.DirectorySeparatorChar}surfsup.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={GetDefaultDbPath()}");
            }
        }
    }
}
