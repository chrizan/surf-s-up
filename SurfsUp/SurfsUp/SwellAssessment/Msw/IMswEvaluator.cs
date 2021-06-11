using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Msw
{
    public enum MswStrategy
    {
        Italy,
        France,
        Spain
    }

    public interface IMswEvaluator
    {
        ISet<DayOfWeek> EvaluateMswData(SwellData swellData, MswStrategy mswStrategy);
    }
}