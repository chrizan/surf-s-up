using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Contracts
{
    public interface IDatabaseService
    {
        Task<List<string>> GetAllSurfSpotsAsync();

        Task AddSurfSpotAsync(string url);

        Task RemoveSurfSpotAsync(string url);
    }
}
