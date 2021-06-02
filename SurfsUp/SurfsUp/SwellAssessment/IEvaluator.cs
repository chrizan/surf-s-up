using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Strategy;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public interface IEvaluator
    {
        IStrategy Strategy { set; }

        void Evaluate(SwellData swellData);
    }
}