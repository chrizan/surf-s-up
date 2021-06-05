using SurfsUp.DataProvider.Models;
using System;
using System.Collections.Generic;

namespace SurfsUp.SurfsUp.SwellAssessment
{
    public enum Strategy
    {
        Italy,
        France,
        Spain
    }

    public interface IEvaluator
    {
        ISet<DayOfWeek> Evaluate(SwellData swellData, Strategy strategy);
    }
}