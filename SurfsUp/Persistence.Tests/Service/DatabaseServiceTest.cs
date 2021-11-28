using FluentAssertions;
using SurfsUp.Persistence.Model;
using SurfsUp.Persistence.Service;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SurfsUp.Persistence.Tests.Service
{
    public class DatabaseServiceTest
    {
        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_MswSurfspot()
        {
            // Arrange
            var mswSurfSpot = new MswSurfSpot() { Url = "Msw Test Url", Name = "Msw Test Name", FullStars = 2, BlurredStars = 1 };
            var dbService = new DatabaseService();

            // Act
            await dbService.AddMswSurfSpotAsync(mswSurfSpot);
            var allMswSurfSpots = await dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allMswSurfSpots.Any(s => s.Url == mswSurfSpot.Url).Should().BeTrue();

            // Act
            await dbService.RemoveMswSurfSpotAsync(mswSurfSpot);
            allMswSurfSpots = await dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allMswSurfSpots.Any(s => s.Url == mswSurfSpot.Url).Should().BeFalse();
        }

        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_BafuSurfspot()
        {
            // Arrange
            var bafuSurfSpot = new BafuSurfSpot() { Url = "Bafu Test Url", Name = "Bafu Test Name", Outflow = 250 };
            var dbService = new DatabaseService();

            // Act
            await dbService.AddBafuSurfSpotAsync(bafuSurfSpot);
            var allBafuSurfSpots = await dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allBafuSurfSpots.Any(s => s.Url == bafuSurfSpot.Url).Should().BeTrue();

            // Act
            await dbService.RemoveBafuSurfSpotAsync(bafuSurfSpot);
            allBafuSurfSpots = await dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allBafuSurfSpots.Any(s => s.Url == bafuSurfSpot.Url).Should().BeFalse();
        }
    }
}
