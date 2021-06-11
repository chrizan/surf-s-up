using SurfsUp.DataProvider.Models;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;

namespace SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu
{
    public interface IBafuStrategy
    {
        BafuStrategy BafuStrategy { get; }

        bool IsFiring(BafuData bafuData);
    }
}
