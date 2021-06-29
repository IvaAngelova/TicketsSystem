using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;
using System.Security.Claims;

namespace TicketSystem.Pages.TicketStatuses
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "Email");
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketID", "Title");

            TicketStatus = new TicketStatus
            {
                EmployeeID = _context.Employee
                   .FirstOrDefault(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).EmployeeID
            };

            return Page();
        }

        [BindProperty]
        public TicketStatus TicketStatus { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TicketStatus.Add(TicketStatus);

            if (TicketStatus.StatusName == "Приет")
            {
                var ticket = _context.Ticket
                    .Where(t => t.TicketID == TicketStatus.TicketID)
                    .FirstOrDefault();

                ticket.AcceptedATicketID = TicketStatus.EmployeeID;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
