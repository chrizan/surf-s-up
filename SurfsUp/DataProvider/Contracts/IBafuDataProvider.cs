using SurfsUp.DataProvider.Providers.Bafu;

namespace SurfsUp.DataProvider.Contracts
{
    public interface IBafuDataProvider
    {
        Task<BafuData> GetOutflowData(string spotUrl);
    }
}
