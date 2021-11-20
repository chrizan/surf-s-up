using Database.Model;
using SurfsUp.DataProvider.Models;

namespace SurfsUp.SurfsUp.SwellAssessment.Bafu
{
    public interface IBafuEvaluator
    {
        bool IsFiring(BafuData bafuData, BafuSurfSpot bafuSurfSpot);
    }
}