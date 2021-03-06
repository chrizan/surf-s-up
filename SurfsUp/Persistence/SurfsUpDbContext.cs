using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence
{
    public class SurfsUpDbContext : DbContext
    {
        public DbSet<MswSurfSpot> MswSurfSpots { get; set; }
        public DbSet<BafuSurfSpot> BafuSurfSpots { get; set; }

        public string DbPath { get; private set; }

        public SurfsUpDbContext()
        {
            /*
             * Environment.SpecialFolder.LocalApplicationData:
             * Windows: User\AppData\Local
             * Linux: home/.local/share
             */
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}surfsup.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}
