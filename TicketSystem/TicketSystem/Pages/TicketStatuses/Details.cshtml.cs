using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketStatuses
{
    public class DetailsModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public DetailsModel(TicketSystemContext context)
        {
            _context = context;
        }

        public TicketStatus TicketStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TicketStatus = await _context.TicketStatus
                .Include(t => t.Employee)
                .Include(t => t.Ticket).FirstOrDefaultAsync(m => m.TicketStatusID == id);

            if (TicketStatus == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
