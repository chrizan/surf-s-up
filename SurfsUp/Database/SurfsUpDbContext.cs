using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class SurfsUpDbContext : DbContext
    {
        public DbSet<SurfSpot> SurfSpots { get; set; }

        public string DbPath { get; private set; }

        public SurfsUpDbContext()
        {
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
