using Database.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Contracts
{
    public interface IDatabaseService
    {
        Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync();

        Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync();

        Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);

        Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);
    }
}
