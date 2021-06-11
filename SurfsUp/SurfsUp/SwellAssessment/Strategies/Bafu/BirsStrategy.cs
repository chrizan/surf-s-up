using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu
{
    public class BirsStrategy : IBafuStrategy
    {
        private const double InterestingWaterLevel = 70;

        public BafuStrategy BafuStrategy => BafuStrategy.Birs;

        public bool IsFiring(BafuData bafuData)
        {
            return bafuData.OutflowCurrent > InterestingWaterLevel || bafuData.OutflowMax24hours > InterestingWaterLevel;
        }
    }
}
