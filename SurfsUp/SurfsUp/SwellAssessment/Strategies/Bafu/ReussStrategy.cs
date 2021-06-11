using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu
{
    public class ReussStrategy : IBafuStrategy
    {
        private const double InterestingWaterLevel = 250;

        public BafuStrategy BafuStrategy => BafuStrategy.Reuss;

        public bool IsFiring(BafuData bafuData)
        {
            return bafuData.OutflowCurrent > InterestingWaterLevel || bafuData.OutflowMax24hours > InterestingWaterLevel;
        }
    }
}
