using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TicketSystem.Pages.Tickets
{
    [Authorize(Roles = TicketSystemRoles.Admin)]
    public class DeleteModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public DeleteModel(TicketSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Ticket
                .Include(t => t.CreatorTicket)
                .Include(t => t.TicketCategory)
                .Include(t => t.TicketPriority)
                .Include(t => t.AcceptedATicket).FirstOrDefaultAsync(m => m.TicketID == id);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Ticket.FindAsync(id);

            if (Ticket != null)
            {
                _context.Ticket.Remove(Ticket);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
