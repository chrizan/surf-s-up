using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence.Contracts
{
    public interface IDatabaseService
    {
        Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync();

        Task<MswSurfSpot> GetMswSurfSpotAsync(int id);

        Task ChangeMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot);

        Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync();

        Task<BafuSurfSpot> GetBafuSurfSpotAsync(int id);

        Task ChangeBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);

        Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);

        Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot);
    }
}
