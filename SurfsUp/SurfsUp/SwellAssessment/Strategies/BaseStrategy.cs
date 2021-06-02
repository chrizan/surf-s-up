using SurfsUp.DataProvider.Models;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public abstract class BaseStrategy
    {
        protected bool HasDaylight(ForecastHour hour)
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
