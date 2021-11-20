using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Database.Service.Tests
{
    public class DatabaseServiceTest
    {
        [Fact]
        public async Task Test_DatabaseService_Add_And_Remove_Surfspot()
        {
            // Arrange
            const string SpotUrl = "Test Url";
            var dbService = new DatabaseService();

            // Act
            await dbService.AddSurfSpotAsync(SpotUrl);
            var allSurfSpots = await dbService.GetAllSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().Contain(s => s == SpotUrl);

            // Act
            await dbService.RemoveSurfSpotAsync(SpotUrl);
            allSurfSpots = await dbService.GetAllSurfSpotsAsync();

            // Assert
            allSurfSpots.Should().NotContain(s => s == SpotUrl);
        }
    }
}
