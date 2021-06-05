using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SurfsUp.DataProvider.Models
{
    [Serializable]
    public sealed class DailySwellData : Dictionary<ForecastHour, HourlySwellData>
    {
        public DailySwellData() { }

        private DailySwellData(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
