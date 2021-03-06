using FluentAssertions;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Providers.Msw;
using System;
using Xunit;

namespace SurfsUp.DataProvider.Tests.Providers.Msw
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
            MswSwellData mswSwellData = mswDataProvider.GetMswSwellDataFromFile(MswVieuxBoucauForecast);

            // Assert
            mswSwellData.Count.Should().Be(7);

            for (int day = 1; day <= mswSwellData.Count; day++)
            {
                bool hasValueForDay = mswSwellData.TryGetValue(day, out MswDailySwellData mswDailySwellData);
                hasValueForDay.Should().BeTrue();

                AssertMswDailySwellData(day, mswDailySwellData);
            }
        }

        private static void AssertMswDailySwellData(int day, MswDailySwellData mswDailySwellData)
        {
            mswDailySwellData.Count.Should().Be(8);

            for (int forecastHour = 1; forecastHour <= mswDailySwellData.Count; forecastHour++)
            {
                bool hasValueForHour = mswDailySwellData.TryGetValue((ForecastHour)forecastHour, out MswHourlySwellData mswHourlySwellData);
                hasValueForHour.Should().BeTrue();

                switch (day)
                {
                    case 1:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 19, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 2:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 20, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 3:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 21, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 4:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(5);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 22, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 5:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(1);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 23, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 6:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(1);
                                mswHourlySwellData.BlurredStars.Should().Be(1);
                                mswHourlySwellData.EmptyStars.Should().Be(3);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 24, 21, 00, 00));
                                break;

                            default:
                                break;
                        }
                        break;

                    case 7:
                        switch (forecastHour)
                        {
                            case (int)ForecastHour.Hour_12am:
                                mswHourlySwellData.FullStars.Should().Be(3);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(2);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25));
                                break;

                            case (int)ForecastHour.Hour_3am:
                                mswHourlySwellData.FullStars.Should().Be(4);
                                mswHourlySwellData.BlurredStars.Should().Be(0);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 03, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6am:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(4);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 06, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9am:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(4);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 09, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_Noon:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(4);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 12, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_3pm:
                                mswHourlySwellData.FullStars.Should().Be(0);
                                mswHourlySwellData.BlurredStars.Should().Be(4);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 15, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_6pm:
                                mswHourlySwellData.FullStars.Should().Be(2);
                                mswHourlySwellData.BlurredStars.Should().Be(2);
                                mswHourlySwellData.EmptyStars.Should().Be(1);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 18, 00, 00));
                                break;

                            case (int)ForecastHour.Hour_9pm:
                                mswHourlySwellData.FullStars.Should().Be(1);
                                mswHourlySwellData.BlurredStars.Should().Be(4);
                                mswHourlySwellData.EmptyStars.Should().Be(0);
                                mswHourlySwellData.Timestamp.Should().Be(new DateTime(2020, 11, 25, 21, 00, 00));
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
