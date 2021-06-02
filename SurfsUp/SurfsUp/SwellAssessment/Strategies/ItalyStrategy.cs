using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public class ItalyStrategy : BaseStrategy, IStrategy
    {
        public Strategy Strategy => Strategy.Italy;

        public ISet<DateTime> Assess(SwellData swellData)
        {
            ISet<DateTime> swellDates = new HashSet<DateTime>();

            foreach (var day in swellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key) && hour.Value.EmptyStars <= 4)
                    {
                        swellDates.Add(day.Value.Date);
                    }
                }
            }

            return swellDates;
        }
    }
}
