using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence.Service
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDbContextFactory<SurfsUpDbContext> _contextFactory;

        public DatabaseService(IDbContextFactory<SurfsUpDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync()
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.MswSurfSpots.ToListAsync();
        }

        public async Task<MswSurfSpot> GetMswSurfSpotAsync(int id)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.MswSurfSpots.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task ChangeMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Attach(mswSurfSpot).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Add(mswSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Remove(mswSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync()
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.BafuSurfSpots.ToListAsync();
        }

        public async Task<BafuSurfSpot> GetBafuSurfSpotAsync(int id)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.BafuSurfSpots.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task ChangeBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Attach(bafuSurfSpot).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Add(bafuSurfSpot);
            await db.SaveChangesAsync();
        }

        public async Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            db.Remove(bafuSurfSpot);
            await db.SaveChangesAsync();
        }
    }
}
