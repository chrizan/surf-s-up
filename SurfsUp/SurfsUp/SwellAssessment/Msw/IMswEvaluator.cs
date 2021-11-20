using Database.Model;
using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment.Msw
{
    public interface IMswEvaluator
    {
        ISet<DayOfWeek> EvaluateMswData(SwellData swellData, MswSurfSpot mswSurfSpot);
    }
}