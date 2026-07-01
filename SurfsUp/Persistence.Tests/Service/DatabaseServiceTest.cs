using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SurfsUp.Persistence.Model;
using SurfsUp.Persistence.Service;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SurfsUp.Persistence.Tests.Service
{
    public class DatabaseServiceTest : IDisposable
    {
        private readonly string _dbPath;
        private readonly DatabaseService _dbService;

        public DatabaseServiceTest()
        {
            _dbPath = Path.Combine(Path.GetTempPath(), $"surfsup-test-{Guid.NewGuid()}.db");
            var options = new DbContextOptionsBuilder<SurfsUpDbContext>()
                .UseSqlite($"Data Source={_dbPath}")
                .Options;
            var contextFactory = new PooledDbContextFactory<SurfsUpDbContext>(options);

            using (var db = contextFactory.CreateDbContext())
            {
                db.Database.Migrate();
            }

            _dbService = new DatabaseService(contextFactory);
        }

        public void Dispose()
        {
            File.Delete(_dbPath);
        }

        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_MswSurfspot()
        {
            // Arrange
            var mswSurfSpot = new MswSurfSpot() { Url = "Msw Test Url", Name = "Msw Test Name", FullStars = 2, BlurredStars = 1 };

            // Act
            await _dbService.AddMswSurfSpotAsync(mswSurfSpot);
            var allMswSurfSpots = await _dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allMswSurfSpots.Any(s => s.Url == mswSurfSpot.Url).Should().BeTrue();

            // Act
            await _dbService.RemoveMswSurfSpotAsync(mswSurfSpot);
            allMswSurfSpots = await _dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allMswSurfSpots.Any(s => s.Url == mswSurfSpot.Url).Should().BeFalse();
        }

        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_BafuSurfspot()
        {
            // Arrange
            var bafuSurfSpot = new BafuSurfSpot() { Url = "Bafu Test Url", Name = "Bafu Test Name", Outflow = 250 };

            // Act
            await _dbService.AddBafuSurfSpotAsync(bafuSurfSpot);
            var allBafuSurfSpots = await _dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allBafuSurfSpots.Any(s => s.Url == bafuSurfSpot.Url).Should().BeTrue();

            // Act
            await _dbService.RemoveBafuSurfSpotAsync(bafuSurfSpot);
            allBafuSurfSpots = await _dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allBafuSurfSpots.Any(s => s.Url == bafuSurfSpot.Url).Should().BeFalse();
        }
    }
}
