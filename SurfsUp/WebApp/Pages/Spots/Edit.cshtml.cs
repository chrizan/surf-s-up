using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class EditModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public EditModel(IDatabaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        [BindProperty]
        public MswSurfSpot MswSurfSpot { get; set; }

        public async Task<IActionResult> OnGetAsync(string? url)
        {
            if (url == null)
            {
                return NotFound();
            }

            MswSurfSpot = await _dataBaseService.GetMswSurfSpotAsync(url);

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
                if (!MovieExists(MswSurfSpot.Url))
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

        private bool MovieExists(string url)
        {
            List<MswSurfSpot> surfSpots = _dataBaseService.GetAllMswSurfSpotsAsync().Result;
            return surfSpots.Any(s => s.Url == url);
        }
    }
}
