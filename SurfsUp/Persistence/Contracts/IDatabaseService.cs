using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence.Contracts
{
    public interface IDatabaseService
    {
        Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync();

        Task<MswSurfSpot> GetMswSurfSpotAsync(string url);

        Task ChangeMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync();

        Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);

        Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);
    }
}
