using SurfsUp.DataProvider.Providers.Msw;

namespace SurfsUp.DataProvider.Contracts
{
    public interface IMswDataProvider
    {
        Task<MswSwellData> GetMswSwellDataFromWeb(string spotUrl);
        MswSwellData GetMswSwellDataFromFile(string filePath);
    }
}
