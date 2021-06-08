using SurfsUp.DataProvider.Models;
using System.Threading.Tasks;

namespace SurfsUp.DataProvider.Contracts
{
    public interface IBafuDataProvider
    {
        Task<BafuData> GetOutflowData(string bafuUrl);
    }
}
