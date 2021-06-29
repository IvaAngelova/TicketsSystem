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

namespace TicketSystem.Pages.Tickets
{
    [Authorize(Roles = TicketSystemRoles.AdminOrManager)]
    public class TicketsReportModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public TicketsReportModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<TicketsReport> TicketsReports { get; set; }

        [BindProperty]
        public DateTime? ReportyByDate { get; set; }

        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Ticket> ticketIQ = from t in _context.Ticket
                                          select t;

            if (User.Identity.IsAuthenticated && User.IsInRole("Служител"))
            {
                var employeeId = _context.Employee.FirstOrDefault(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .EmployeeID;

                ticketIQ = ticketIQ.Where(t => t.CreatorID == employeeId);
            }

            int pageSize = 5;

            if (ReportyByDate != null)
            {
                TicketsReports = await PaginatedList<TicketsReport>
                   .CreateAsynx(ticketIQ
                   .Include(t => t.CreatorTicket)
                   .Include(t => t.TicketCategory)
                   .Include(t => t.TicketPriority)
                   .Include(t => t.AcceptedATicket)
                   .Where(t => t.OpenDate.Date == ReportyByDate.Value.Date)
                   .Select(t => new TicketsReport
                   {
                       Title = t.Title,
                       Creator = t.CreatorTicket.FirstName + " " + t.CreatorTicket.LastName,
                       Accepted = t.AcceptedATicket.FirstName + " " + t.AcceptedATicket.LastName,
                       OpenDate = t.OpenDate,
                       TicketPriority = t.TicketPriority.PriorityType
                   })
                   .OrderBy(t => t.TicketPriority)
                   .AsNoTracking(), pageIndex ?? 1, pageSize);
            }
            else
            {
                TicketsReports = await PaginatedList<TicketsReport>
                    .CreateAsynx(ticketIQ
                    .Include(t => t.CreatorTicket)
                    .Include(t => t.TicketCategory)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.AcceptedATicket)
                    .Select(t => new TicketsReport
                    {
                        Title = t.Title,
                        Creator = t.CreatorTicket.FirstName + " " + t.CreatorTicket.LastName,
                        Accepted = t.AcceptedATicket.FirstName + " " + t.AcceptedATicket.LastName,
                        OpenDate = t.OpenDate,
                        TicketPriority = t.TicketPriority.PriorityType
                    })
                    .OrderBy(t => t.TicketPriority)
                    .AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }

        public async Task<IActionResult> OnPostAsync(DateTime? date)
        {
            ReportyByDate = date;

            var ticketsData = new List<TicketsReport>();

            if (ReportyByDate != null)
            {
                ticketsData = _context
                .Ticket
                .Where(t => t.OpenDate.Date == ReportyByDate.Value.Date)
                .Select(t => new TicketsReport
                {
                    Title = t.Title,
                    Creator = t.CreatorTicket.FirstName + " " + t.CreatorTicket.LastName,
                    Accepted = t.AcceptedATicket.FirstName + " " + t.AcceptedATicket.LastName,
                    OpenDate = t.OpenDate,
                    TicketPriority = t.TicketPriority.PriorityType
                })
                .OrderBy(t => t.TicketPriority)
                .ToList();
            }
            else
            {
                ticketsData = _context
                .Ticket
                .Select(t => new TicketsReport
                {
                    Title = t.Title,
                    Creator = t.CreatorTicket.FirstName + " " + t.CreatorTicket.LastName,
                    Accepted = t.AcceptedATicket.FirstName + " " + t.AcceptedATicket.LastName,
                    OpenDate = t.OpenDate,
                    TicketPriority = t.TicketPriority.PriorityType
                })
                .OrderBy(t => t.TicketPriority)
                .ToList();
            }

            try
            {
                return DownloadCommaSeperatedFile(ticketsData);
            }
            catch (Exception)
            {
                ErrorMessage = "Error with report";
            }

            return Page();
        }

        public IActionResult DownloadCommaSeperatedFile(IEnumerable<TicketsReport> data)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "TicketsReport.xlsx";

            using var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Tickets");

            worksheet.Range("A1:E1").Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;

            worksheet.Cell(1, 1).Value = "All Tickets";

            worksheet.Cell(2, 1).Value = "Title";
            worksheet.Cell(2, 2).Value = "Creator";
            worksheet.Cell(2, 3).Value = "Accepted";
            worksheet.Cell(2, 4).Value = "OpenDate";
            worksheet.Cell(2, 5).Value = "TicketPriority";

            for (int i = 1; i <= data.Count(); i++)
            {
                worksheet.Cell(i + 2, 1).Value = data.ElementAt(i - 1).Title;
                worksheet.Cell(i + 2, 2).Value = data.ElementAt(i - 1).Creator;
                worksheet.Cell(i + 2, 3).Value = data.ElementAt(i - 1).Accepted;
                worksheet.Cell(i + 2, 4).Value = data.ElementAt(i - 1).OpenDate;
                worksheet.Cell(i + 2, 5).Value = data.ElementAt(i - 1).TicketPriority;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            var content = stream.ToArray();

            return File(content, contentType, fileName);
        }
    }
}
