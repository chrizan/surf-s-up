using Database.Model;
using SurfsUp.DataProvider.Models;

namespace SurfsUp.SurfsUp.SwellAssessment.Bafu
{
    public class BafuEvaluator : IBafuEvaluator
    {
        public bool IsFiring(BafuData bafuData, BafuSurfSpot bafuSurfSpot)
        {
            return bafuData.OutflowCurrent > bafuSurfSpot.Outflow || bafuData.OutflowMax24hours > bafuSurfSpot.Outflow;
        }
    }
}
