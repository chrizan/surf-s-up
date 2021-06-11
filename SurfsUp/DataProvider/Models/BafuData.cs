using System;
using System.Collections.Generic;

namespace SurfsUp.DataProvider.Models
{
    public class BafuData
    {
        public double OutflowCurrent { get; set; }

        public double OutflowMax24hours { get; set; }

        public double DegreeCelsius { get; set; }

        public ISet<DayOfWeek> Dates { get; set; }
    }
}
