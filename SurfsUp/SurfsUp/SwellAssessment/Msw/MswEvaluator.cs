using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Strategies.Msw;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurfsUp.SurfsUp.SwellAssessment.Msw
{
    public class MswEvaluator : IMswEvaluator
    {
        private readonly IEnumerable<IMswStrategy> _strategies;

        public MswEvaluator(IEnumerable<IMswStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ISet<DayOfWeek> EvaluateMswData(SwellData swellData, MswStrategy mswStrategy)
        {
            return _strategies.FirstOrDefault(s => s.MswStrategy == mswStrategy)?.Assess(swellData);
        }
    }
}
