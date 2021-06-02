using SurfsUp.DataProvider.Models;
using System;
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
        ISet<DateTime> Evaluate(SwellData swellData, Strategy strategy);
    }
}