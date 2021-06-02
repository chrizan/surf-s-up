using SurfsUp.DataProvider.Models;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public interface IStrategy
    {
        Strategy Strategy { get; }

        ISet<string> Assess(SwellData swellData);
    }
}
