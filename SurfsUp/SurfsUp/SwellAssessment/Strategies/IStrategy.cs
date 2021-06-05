using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies
{
    public interface IStrategy
    {
        Strategy Strategy { get; }

        ISet<DayOfWeek> Assess(SwellData swellData);
    }
}
