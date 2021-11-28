using SurfsUp.DataProvider.Providers.Msw;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.SwellAssessment.Msw
{
    public interface IMswEvaluator
    {
        ISet<DayOfWeek> EvaluateMswData(MswSwellData mswSwellData, MswSurfSpot mswSurfSpot);
    }
}