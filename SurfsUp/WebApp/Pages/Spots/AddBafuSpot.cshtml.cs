using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Model;

namespace SurfsUp.WebApp.Pages.Spots
{
    public class AddBafuSpotModel : PageModel
    {
        private readonly IDatabaseService _dataBaseService;

        public AddBafuSpotModel(IDatabaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BafuSurfSpot BafuSurfSpot { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _dataBaseService.AddBafuSurfSpotAsync(BafuSurfSpot);

            return RedirectToPage("./Index");
        }
    }
}
