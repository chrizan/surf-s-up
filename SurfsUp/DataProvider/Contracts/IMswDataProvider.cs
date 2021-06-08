using SurfsUp.DataProvider.Models;
using System.Threading.Tasks;

namespace SurfsUp.DataProvider.Contracts
{
    public interface IMswDataProvider
    {
        Task<SwellData> GetSwellDataFromWeb(string spotUrl);
        SwellData GetSwellDataFromFile(string filePath);
    }
}
