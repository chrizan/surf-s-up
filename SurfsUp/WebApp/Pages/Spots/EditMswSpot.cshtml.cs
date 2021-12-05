using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class EditMswSpotModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public EditMswSpotModel(IDatabaseService databaseService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _dataBaseService.ChangeMswSurfSpotAsync(MswSurfSpot);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurfSpotExists(MswSurfSpot.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SurfSpotExists(int id)
        {
            return _dataBaseService.GetAllMswSurfSpotsAsync().Result.Any(s => s.Id == id);
        }
    }
}
