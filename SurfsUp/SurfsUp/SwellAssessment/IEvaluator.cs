using SurfsUp.SurfsUp.SwellAssessment.Strategy;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public interface IEvaluator
    {
        IStrategy Strategy { set; }

        void Evaluate();
    }
}