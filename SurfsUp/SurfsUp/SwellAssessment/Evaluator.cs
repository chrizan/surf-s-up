using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public class Evaluator : IEvaluator
    {
        private readonly IEnumerable<IStrategy> _strategies;

        public Evaluator(IEnumerable<IStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ISet<DayOfWeek> Evaluate(SwellData swellData, Strategy strategy)
        {
            return _strategies.FirstOrDefault(s => s.Strategy == strategy)?.Assess(swellData);
        }
    }
}
