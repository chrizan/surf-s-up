using Microsoft.AspNetCore.Mvc.RazorPages;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class IndexModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public IndexModel(IDatabaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        public IList<MswSurfSpot> MswSurfSpots { get; set; }

        public IList<BafuSurfSpot> BafuSurfSpots { get; set; }

        public async Task OnGetAsync()
        {
            MswSurfSpots = await _dataBaseService.GetAllMswSurfSpotsAsync();
            BafuSurfSpots = await _dataBaseService.GetAllBafuSurfSpotsAsync();
        }
    }
}
