using SurfsUp.DataProvider.Providers.Bafu;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.SwellAssessment.Bafu
{
    public class BafuEvaluator : IBafuEvaluator
    {
        public bool IsFiring(BafuData bafuData, BafuSurfSpot bafuSurfSpot)
        {
            return bafuData.OutflowCurrent > bafuSurfSpot.Outflow || bafuData.OutflowMax24hours > bafuSurfSpot.Outflow;
        }
    }
}
