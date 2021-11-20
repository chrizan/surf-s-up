using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Contracts
{
    public interface IDatabaseService
    {
        Task<List<string>> GetAllMswSurfSpotsAsync();

        Task AddMswSurfSpotAsync(string url);

        Task RemoveMswSurfSpotAsync(string url);

        Task<List<string>> GetAllBafuSurfSpotsAsync();

        Task AddBafuSurfSpotAsync(string url);

        Task RemoveBafuSurfSpotAsync(string url);
    }
}
