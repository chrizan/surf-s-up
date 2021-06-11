using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Msw;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Msw
{
    public interface IMswStrategy
    {
        MswStrategy MswStrategy { get; }

        ISet<DayOfWeek> Assess(SwellData swellData);
    }
}
