using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public CreateModel(TicketSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string userId, string email)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Employee = new Employee
                {
                    UserId = userId,
                    Email = email
                };
            }

            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "DepartmentName");

            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
