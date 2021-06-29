using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;
using System.Security.Claims;

namespace TicketSystem.Pages.Tickets
{
    [Authorize(Roles = TicketSystemRoles.Employee)]
    public class CreateModel : PageModel
    {
        private readonly TicketSystemContext _context;
        private readonly IWebHostEnvironment _he;

        public CreateModel(TicketSystemContext context,
            IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        public IActionResult OnGet()
        {
            ViewData["CreatorID"] = new SelectList(_context.Employee, "EmployeeID", "Email");
            ViewData["TicketCategoryID"] = new SelectList(_context.TicketCategory, "TicketCategoryID", "CategoryName");
            ViewData["TicketPriorityID"] = new SelectList(_context.TicketPriority, "TicketPriorityID", "PriorityType");
            ViewData["АcceptedАТicketID"] = new SelectList(_context.Employee, "EmployeeID", "Email");
           
            Ticket = new Ticket
            {
                CreatorID = _context.Employee
                   .FirstOrDefault(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).EmployeeID
            };

            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb UploadFiles { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadFiles != null)
            {
                using var memoryStream = new MemoryStream();

                if (UploadFiles.FormFile!=null)
                {
                    await UploadFiles.FormFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        Ticket.Photo = memoryStream.ToArray();
                    }
                }
            }

            _context.Ticket.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
