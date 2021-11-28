using SurfsUp.DataProvider.Providers.Bafu;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.SwellAssessment.Bafu
{
    public interface IBafuEvaluator
    {
        bool IsFiring(BafuData bafuData, BafuSurfSpot bafuSurfSpot);
    }
}