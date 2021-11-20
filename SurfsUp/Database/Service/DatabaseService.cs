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
        public async Task AddSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Add(new SurfSpot { Url = url });
            await db.SaveChangesAsync();
        }

        public async Task RemoveSurfSpotAsync(string url)
        {
            using var db = new SurfsUpDbContext();
            db.Remove(new SurfSpot() { Url = url });
            await db.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllSurfSpotsAsync()
        {
            using var db = new SurfsUpDbContext();
            return await db.SurfSpots.Select(s => s.Url).ToListAsync();
        }
    }
}
