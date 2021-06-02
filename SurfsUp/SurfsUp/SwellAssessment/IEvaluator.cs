using SurfsUp.DataProvider.Models;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public enum Strategy
    {
        Italy,
        France
    }

    public interface IEvaluator
    {
        ISet<string> Evaluate(SwellData swellData, Strategy strategy);
    }
}