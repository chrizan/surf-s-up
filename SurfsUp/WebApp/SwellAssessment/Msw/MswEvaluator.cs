using SurfsUp.DataProvider.Providers.Msw;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.SwellAssessment.Msw
{
    public class MswEvaluator : IMswEvaluator
    {
        public ISet<DayOfWeek> EvaluateMswData(MswSwellData mswSwellData, MswSurfSpot mswSurfSpot)
        {
            ISet<DayOfWeek> swellDates = new HashSet<DayOfWeek>();

            foreach (var day in mswSwellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key) && hour.Value.FullStars >= mswSurfSpot.FullStars && hour.Value.BlurredStars >= mswSurfSpot.BlurredStars)
                    {
                        swellDates.Add(hour.Value.Timestamp.DayOfWeek);
                    }
                }
            }

            return swellDates;
        }

        private static bool HasDaylight(ForecastHour hour)
        {
            bool hasDaylight = default;
            switch (hour)
            {
                case ForecastHour.Hour_12am:
                    hasDaylight = false;
                    break;
                case ForecastHour.Hour_3am:
                    hasDaylight = false;
                    break;
                case ForecastHour.Hour_6am:
                    hasDaylight = true;
                    break;
                case ForecastHour.Hour_9am:
                    hasDaylight = true;
                    break;
                case ForecastHour.Hour_Noon:
                    hasDaylight = true;
                    break;
                case ForecastHour.Hour_3pm:
                    hasDaylight = true;
                    break;
                case ForecastHour.Hour_6pm:
                    hasDaylight = true;
                    break;
                case ForecastHour.Hour_9pm:
                    hasDaylight = true;
                    break;
                default:
                    break;
            }
            return hasDaylight;
        }
    }
}
