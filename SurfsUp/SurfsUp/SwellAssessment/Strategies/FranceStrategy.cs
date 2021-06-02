using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public class FranceStrategy : BaseStrategy, IStrategy
    {
        public Strategy Strategy => Strategy.France;

        public ISet<string> Assess(SwellData swellData)
        {
            ISet<string> swellDates = new HashSet<string>();

            foreach (var day in swellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key))
                    {
                        if (hour.Value.EmptyStars <= 2)
                        {
                            swellDates.Add(day.Value.Date);
                        }
                    }
                }
            }

            return swellDates;
        }
    }
}
