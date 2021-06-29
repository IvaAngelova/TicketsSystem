using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

using ClosedXML.Excel;

namespace TicketSystem.Pages.Employees
{
    [Authorize(Roles = TicketSystemRoles.AdminOrManagerOrEmp)]
    public class DetailsModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public DetailsModel(TicketSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                Employee = await _context.Employee
                    .Include(e => e.АcceptedАТickets)
                    .Include(e => e.CreatorTickets)
                    .Include(e => e.Department).FirstOrDefaultAsync(m => m.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            else
            {
                Employee = await _context.Employee
                    .Include(e => e.АcceptedАТickets)
                    .Include(e => e.CreatorTickets)
                    .Include(e => e.Department).FirstOrDefaultAsync(m => m.EmployeeID == id);
            }
            if (Employee == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var acceptedTicketsEmployeeReport = _context
                .Ticket
                .Where(t => t.AcceptedATicketID == Employee.EmployeeID)
                .Select(e => new TicketsEmployeeReport
                {
                    FirstName = e.AcceptedATicket.FirstName,
                    LastName = e.AcceptedATicket.LastName,
                    Email = e.AcceptedATicket.Email,
                    TicketTitle = e.Title,
                    OpenDate = e.OpenDate
                })
                .OrderBy(e => e.OpenDate)
                .ToList();

            var createdTicketsEmployeeReport = _context
                .Ticket
                .Where(t => t.CreatorID == Employee.EmployeeID)
                .Select(e => new TicketsEmployeeReport
                {
                    FirstName = e.CreatorTicket.FirstName,
                    LastName = e.CreatorTicket.LastName,
                    Email = e.CreatorTicket.Email,
                    TicketTitle = e.Title,
                    OpenDate = e.OpenDate
                })
                .OrderBy(e => e.OpenDate)
                .ToList();
            try
            {
                if (Employee.Email.Contains("admin"))
                {
                    return DownloadCommaSeperatedFile(acceptedTicketsEmployeeReport);
                }
                else
                {
                    return DownloadCommaSeperatedFile(createdTicketsEmployeeReport);
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Error with report";
            }

            return Page();
        }

        public IActionResult DownloadCommaSeperatedFile(IEnumerable<TicketsEmployeeReport> data)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "EmployeeTicketsReport.xlsx";

            using var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Employees");

            worksheet.Range("A1:E1").Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;

            if (data.Any(e => e.Email.Contains("admin")))
            {
                worksheet.Cell(1, 1).Value = "Accepted tikets of Administrator";
            }
            else if (data.Any(e => e.Email.Contains("employee")))
            {
                worksheet.Cell(1, 1).Value = "Created tikets of Employee";
            }

            worksheet.Cell(2, 1).Value = "FirstName";
            worksheet.Cell(2, 2).Value = "LastName";
            worksheet.Cell(2, 3).Value = "Email";
            worksheet.Cell(2, 4).Value = "TicketTitle";
            worksheet.Cell(2, 5).Value = "OpenDate";

            for (int i = 1; i <= data.Count(); i++)
            {
                worksheet.Cell(i + 2, 1).Value = data.ElementAt(i - 1).FirstName;
                worksheet.Cell(i + 2, 2).Value = data.ElementAt(i - 1).LastName;
                worksheet.Cell(i + 2, 3).Value = data.ElementAt(i - 1).Email;
                worksheet.Cell(i + 2, 4).Value = data.ElementAt(i - 1).TicketTitle;
                worksheet.Cell(i + 2, 5).Value = data.ElementAt(i - 1).OpenDate;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            var content = stream.ToArray();

            return File(content, contentType, fileName);
        }
    }
}
