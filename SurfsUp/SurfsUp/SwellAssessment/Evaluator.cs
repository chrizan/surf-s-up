using SurfsUp.SurfsUp.SwellAssessment.Strategy;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public class Evaluator : IEvaluator
    {
        public IStrategy Strategy { private get; set; }

        public void Evaluate()
        {
            Strategy.Assess();
        }
    }
}
