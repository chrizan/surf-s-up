using FluentAssertions;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Data;
using SurfsUp.DataProvider.Models;
using Xunit;

namespace SurfsUp.DataProvider.Tests.Data
{
    public class MswDataProviderTest
    {
        private const string MswVieuxBoucauForecast = @"TestFile\MswVieuxBoucau.html";

        [Fact]
        public void Test_GetSwellDataFromFile_VieuxBoucau()
        {
            // Arrange
            IDataProvider mswDataProvider = new MswDataProvider();

            // Act
            SwellData swellData = mswDataProvider.GetSwellDataFromFile(MswVieuxBoucauForecast);

            // Assert
            swellData.Count.Should().Be(7);

            for (int day = 1; day <= swellData.Count; day++)
            {
                bool hasValueForDay = swellData.TryGetValue(day, out DailySwellData dailySwellData);
                hasValueForDay.Should().BeTrue();

                AssertDailySwellData(day, dailySwellData);
            }
        }

        private void AssertDailySwellData(int day, DailySwellData dailySwellData)
        {
            dailySwellData.Date.Should().NotBeNullOrEmpty();
            dailySwellData.Count.Should().Be(8);

            for (int forecastHour = 1; forecastHour <= dailySwellData.Count; forecastHour++)
            {
                bool hasValueForHour = dailySwellData.TryGetValue((ForecastHour)forecastHour, out HourlySwellData hourlySwellData);
                hasValueForHour.Should().BeTrue();

                switch (day)
                {
                    case 1:
                        dailySwellData.Date.Should().BeEquivalentTo("Thursday1911");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 2:
                        dailySwellData.Date.Should().BeEquivalentTo("Friday2011");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 3:
                        dailySwellData.Date.Should().BeEquivalentTo("Saturday2111");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 4:
                        dailySwellData.Date.Should().BeEquivalentTo("Sunday2211");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 5:
                        dailySwellData.Date.Should().BeEquivalentTo("Monday2311");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 6:
                        dailySwellData.Date.Should().BeEquivalentTo("Tuesday2411");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 7:
                        dailySwellData.Date.Should().BeEquivalentTo("Wednesday2511");
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
