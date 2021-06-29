using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketStatuses
{
    public class EditModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public EditModel(TicketSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "Email");
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketID", "Title");

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

            _context.Attach(TicketStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketStatusExists(TicketStatus.TicketStatusID))
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

        private bool TicketStatusExists(Guid id)
        {
            return _context.TicketStatus.Any(e => e.TicketStatusID == id);
        }
    }
}
