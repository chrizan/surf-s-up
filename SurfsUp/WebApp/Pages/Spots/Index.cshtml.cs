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

        public IList<MswSurfSpot> Spots { get; set; }

        public async Task OnGetAsync()
        {
            Spots = await _dataBaseService.GetAllMswSurfSpotsAsync();
        }
    }
}
