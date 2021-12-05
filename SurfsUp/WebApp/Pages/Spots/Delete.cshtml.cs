using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class DeleteModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public DeleteModel(IDatabaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        [BindProperty]
        public MswSurfSpot MswSurfSpot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            MswSurfSpot = await _dataBaseService.GetMswSurfSpotAsync(id.Value);

            if (MswSurfSpot == null)
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

            MswSurfSpot = await _dataBaseService.GetMswSurfSpotAsync(id.Value);

            if (MswSurfSpot != null)
            {
                await _dataBaseService.RemoveMswSurfSpotAsync(MswSurfSpot);
            }

            return RedirectToPage("./Index");
        }
    }
}
