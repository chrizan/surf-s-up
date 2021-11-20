using Database.Model;
using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu;
using System.Collections.Generic;
using System.Linq;

namespace SurfsUp.SurfsUp.SwellAssessment.Bafu
{
    public class BafuEvaluator : IBafuEvaluator
    {
        private readonly IEnumerable<IBafuStrategy> _strategies;

        public BafuEvaluator(IEnumerable<IBafuStrategy> strategies)
        {
            _strategies = strategies;
        }

        public bool? IsFiring(BafuData bafuData, BafuStrategy bafuStrategy)
        {
            return _strategies.FirstOrDefault(s => s.BafuStrategy == bafuStrategy)?.IsFiring(bafuData);
        }

        public bool IsFiring(BafuData bafuData, BafuSurfSpot bafuSurfSpot)
        {
            return bafuData.OutflowCurrent > bafuSurfSpot.Outflow || bafuData.OutflowMax24hours > bafuSurfSpot.Outflow;
        }
    }
}
