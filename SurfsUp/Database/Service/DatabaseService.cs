using Database.Contracts;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service
{
    public class DatabaseService : IDatabaseService
    {
        public async Task<List<string>> GetAllMswSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return await db.MswSurfSpots.Select(s => s.Url).ToListAsync();
        }

        public async Task AddMswSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Add(new MswSurfSpot { Url = url });
            await db.SaveChangesAsync();
        }

        public async Task RemoveMswSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(new MswSurfSpot() { Url = url });
            await db.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllBafuSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return await db.BafuSurfSpots.Select(s => s.Url).ToListAsync();
        }

        public async Task AddBafuSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Add(new BafuSurfSpot { Url = url });
            await db.SaveChangesAsync();
        }

        public async Task RemoveBafuSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(new BafuSurfSpot() { Url = url });
            await db.SaveChangesAsync();
        }
    }
}
