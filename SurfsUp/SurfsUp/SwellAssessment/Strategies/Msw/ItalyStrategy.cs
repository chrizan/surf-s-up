using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Msw;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Msw
{
    public class ItalyStrategy : MswBaseStrategy, IMswStrategy
    {
        public MswStrategy MswStrategy => MswStrategy.Italy;

        public ISet<DayOfWeek> Assess(SwellData swellData)
        {
            ISet<DayOfWeek> swellDates = new HashSet<DayOfWeek>();

            foreach (var day in swellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key) && hour.Value.EmptyStars <= 4)
                    {
                        swellDates.Add(hour.Value.Timestamp.DayOfWeek);
                    }
                }
            }

            return swellDates;
        }
    }
}
