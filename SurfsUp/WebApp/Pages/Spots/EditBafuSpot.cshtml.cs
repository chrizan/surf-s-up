using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class EditBafuSpotModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public EditBafuSpotModel(IDatabaseService databaseService)
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
                await _dataBaseService.ChangeBafuSurfSpotAsync(BafuSurfSpot);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurfSpotExists(BafuSurfSpot.Id))
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
            return _dataBaseService.GetAllBafuSurfSpotsAsync().Result.Any(s => s.Id == id);
        }
    }
}
