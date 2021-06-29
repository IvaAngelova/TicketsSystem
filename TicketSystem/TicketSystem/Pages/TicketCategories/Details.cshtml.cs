using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketCategories
{
    public class DetailsModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public DetailsModel(TicketSystemContext context)
        {
            _context = context;
        }

        public TicketCategory TicketCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketCategory = await _context.TicketCategory.FirstOrDefaultAsync(m => m.TicketCategoryID == id);

            if (TicketCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
