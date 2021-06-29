using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketPriorities
{
    public class DetailsModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public DetailsModel(TicketSystemContext context)
        {
            _context = context;
        }

        public TicketPriority TicketPriority { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketPriority = await _context.TicketPriority.FirstOrDefaultAsync(m => m.TicketPriorityID == id);

            if (TicketPriority == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
