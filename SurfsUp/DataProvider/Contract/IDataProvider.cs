using SurfsUp.DataProvider.Models;
using System.Threading.Tasks;

namespace SurfsUp.DataProvider.Contract
{
    public interface IDataProvider
    {
        Task<SwellData> GetSwellDataFromWeb(string spotUrl);
        SwellData GetSwellDataFromFile(string filePath);
    }
}
