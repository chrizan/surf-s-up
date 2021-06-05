using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public class FranceStrategy : BaseStrategy, IStrategy
    {
        public Strategy Strategy => Strategy.France;

        public ISet<DateTime> Assess(SwellData swellData)
        {
            ISet<DateTime> swellDates = new HashSet<DateTime>();

            foreach (var day in swellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key) && hour.Value.FullStars >= 3)
                    {
                        swellDates.Add(hour.Value.Timestamp);
                    }
                }
            }

            return swellDates;
        }
    }
}
