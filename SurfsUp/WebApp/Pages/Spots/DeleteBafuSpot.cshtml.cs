using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class DeleteBafuSpotModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public DeleteBafuSpotModel(IDatabaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        [BindProperty]
        public BafuSurfSpot BafuSurfSpot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            BafuSurfSpot = await _dataBaseService.GetBafuSurfSpotAsync(id.Value);

            if (BafuSurfSpot == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            BafuSurfSpot = await _dataBaseService.GetBafuSurfSpotAsync(id.Value);

            if (BafuSurfSpot != null)
            {
                await _dataBaseService.RemoveBafuSurfSpotAsync(BafuSurfSpot);
            }

            return RedirectToPage("./Index");
        }
    }
}
