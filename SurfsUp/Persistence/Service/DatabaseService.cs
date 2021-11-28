using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence.Service
{
    public class DatabaseService : IDatabaseService
    {
        public async Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return await db.MswSurfSpots.Select(s => s).ToListAsync();
        }

        public async Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Add(mswSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(mswSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return await db.BafuSurfSpots.Select(s => s).ToListAsync();
        }

        public async Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Add(bafuSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(bafuSurfSpot);
            await db.SaveChangesAsync();
        }
    }
}
