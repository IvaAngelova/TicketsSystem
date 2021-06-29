using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketPriorities
{
    public class CreateModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public CreateModel(TicketSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TicketPriority TicketPriority { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TicketPriority.Add(TicketPriority);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
