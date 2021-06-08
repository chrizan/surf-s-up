using FluentAssertions;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Data;
using SurfsUp.DataProvider.Models;
using System;
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
            IMswDataProvider mswDataProvider = new MswDataProvider();

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
            dailySwellData.Count.Should().Be(8);

            for (int forecastHour = 1; forecastHour <= dailySwellData.Count; forecastHour++)
            {
                bool hasValueForHour = dailySwellData.TryGetValue((ForecastHour)forecastHour, out HourlySwellData hourlySwellData);
                hasValueForHour.Should().BeTrue();

                switch (day)
                {
                    case 1:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 2:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 3:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 4:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(5);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 5:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 6:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(1);
                                hourlySwellData.EmptyStars.Should().Be(3);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 7:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                hourlySwellData.FullStars.Should().Be(3);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(2);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                hourlySwellData.FullStars.Should().Be(4);
                                hourlySwellData.BlurredStars.Should().Be(0);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                hourlySwellData.FullStars.Should().Be(0);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                hourlySwellData.FullStars.Should().Be(2);
                                hourlySwellData.BlurredStars.Should().Be(2);
                                hourlySwellData.EmptyStars.Should().Be(1);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                hourlySwellData.FullStars.Should().Be(1);
                                hourlySwellData.BlurredStars.Should().Be(4);
                                hourlySwellData.EmptyStars.Should().Be(0);
                                hourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 21, 00, 00));
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
