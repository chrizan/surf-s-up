using SurfsUp.DataProvider.Models;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategy
{
    public interface IStrategy
    {
        void Assess(SwellData swellData);
    }
}
