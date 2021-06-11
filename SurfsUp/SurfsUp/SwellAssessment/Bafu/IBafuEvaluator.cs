﻿using SurfsUp.DataProvider.Models;

namespace SurfsUp.SurfsUp.SwellAssessment.Bafu
{
    public enum BafuStrategy
    {
        Reuss,
        Birs,
        Thur
    }

    public interface IBafuEvaluator
    {
        bool? IsFiring(BafuData bafuData, BafuStrategy bafuStrategy);
    }
}