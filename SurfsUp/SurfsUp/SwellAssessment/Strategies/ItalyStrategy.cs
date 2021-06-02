using SurfsUp.DataProvider.Models;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public class ItalyStrategy : BaseStrategy, IStrategy
    {
        public Strategy Strategy => Strategy.Italy;

        public ISet<string> Assess(SwellData swellData)
        {
            ISet<string> swellDates = new HashSet<string>();

            foreach (var day in swellData)
            {
                foreach (var hour in day.Value)
                {
                    if (HasDaylight(hour.Key))
                    {
                        if(hour.Value.EmptyStars <= 4)
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
