using System.Runtime.Serialization;

namespace SurfsUp.DataProvider.Providers.Msw
{
    [Serializable]
    public sealed class MswDailySwellData : Dictionary<ForecastHour, MswHourlySwellData>
    {
        public MswDailySwellData() { }

        private MswDailySwellData(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public enum ForecastHour
    {
        Hour_12am = 1,
        Hour_3am = 2,
        Hour_6am = 3,
        Hour_9am = 4,
        Hour_Noon = 5,
        Hour_3pm = 6,
        Hour_6pm = 7,
        Hour_9pm = 8
    }
}
