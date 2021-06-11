using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu
{
    public class ThurStrategy : IBafuStrategy
    {
        private const double InterestingWaterLevel = 400;

        public BafuStrategy BafuStrategy => BafuStrategy.Thur;

        public bool IsFiring(BafuData bafuData)
        {
            return bafuData.OutflowCurrent > InterestingWaterLevel || bafuData.OutflowMax24hours > InterestingWaterLevel;
        }
    }
}
