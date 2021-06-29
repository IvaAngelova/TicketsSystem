using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    [Authorize(Roles = TicketSystemRoles.Employee)]
    public class EditModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public EditModel(TicketSystemContext context)
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

            ViewData["CreatorID"] = new SelectList(_context.Employee, "EmployeeID", "Email");
            ViewData["TicketCategoryID"] = new SelectList(_context.TicketCategory, "TicketCategoryID", "CategoryName");
            ViewData["TicketPriorityID"] = new SelectList(_context.TicketPriority, "TicketPriorityID", "PriorityType");
            ViewData["АcceptedАТicketID"] = new SelectList(_context.Employee, "EmployeeID", "Email");

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

            _context.Attach(Ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(Ticket.TicketID))
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

        private bool TicketExists(Guid id)
        {
            return _context.Ticket.Any(e => e.TicketID == id);
        }
    }
}
