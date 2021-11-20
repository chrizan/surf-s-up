using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Database.Service.Tests
{
    public class DatabaseServiceTest
    {
        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_MswSurfspot()
        {
            // Arrange
            const string MswSpotUrl = "Msw Test Url";
            var dbService = new DatabaseService();

            // Act
            await dbService.AddMswSurfSpotAsync(MswSpotUrl);
            var allSurfSpots = await dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().Contain(s => s == MswSpotUrl);

            // Act
            await dbService.RemoveMswSurfSpotAsync(MswSpotUrl);
            allSurfSpots = await dbService.GetAllMswSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().NotContain(s => s == MswSpotUrl);
        }

        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_BafuSurfspot()
        {
            // Arrange
            const string BafuSpotUrl = "Bafu Test Url";
            var dbService = new DatabaseService();

            // Act
            await dbService.AddBafuSurfSpotAsync(BafuSpotUrl);
            var allSurfSpots = await dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().Contain(s => s == BafuSpotUrl);

            // Act
            await dbService.RemoveBafuSurfSpotAsync(BafuSpotUrl);
            allSurfSpots = await dbService.GetAllBafuSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().NotContain(s => s == BafuSpotUrl);
        }
    }
}
