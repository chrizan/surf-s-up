using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.Persistence.Service
{
    public class DatabaseService : IDatabaseService
    {
        public Task<List<MswSurfSpot>> GetAllMswSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return db.MswSurfSpots.Select(s => s).ToListAsync();
        }

        public Task<MswSurfSpot> GetMswSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            return db.MswSurfSpots.Where(s => s.Url == url).FirstOrDefaultAsync();
        }

        public Task ChangeMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Attach(mswSurfSpot).State = EntityState.Modified;
            return db.SaveChangesAsync();
        }

        public Task AddMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Add(mswSurfSpot);
            return db.SaveChangesAsync();
        }

        public Task RemoveMswSurfSpotAsync(MswSurfSpot mswSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(mswSurfSpot);
            return db.SaveChangesAsync();
        }

        public Task<List<BafuSurfSpot>> GetAllBafuSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return db.BafuSurfSpots.Select(s => s).ToListAsync();
        }

        public Task AddBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Add(bafuSurfSpot);
            return db.SaveChangesAsync();
        }

        public Task RemoveBafuSurfSpotAsync(BafuSurfSpot bafuSurfSpot)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(bafuSurfSpot);
            return db.SaveChangesAsync();
        }
    }
}
