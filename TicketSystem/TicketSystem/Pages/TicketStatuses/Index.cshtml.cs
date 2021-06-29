using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Areas;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketStatuses
{
    [Authorize(Roles = TicketSystemRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<TicketStatus> TicketStatus { get; set; }

        public string StatusNameSort { get; set; }
        public string StatusDateSort { get; set; }
        public string EmployeeSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentFilter = searchString;
            CurrentSort = sortOrder;

            StatusNameSort = sortOrder == "StatusName" ? "StatusName_desc" : "StatusName";
            StatusDateSort = sortOrder == "StatusDate" ? "StatusDate_desc" : "StatusDate";
            EmployeeSort = sortOrder == "Employee" ? "Employee_desc" : "Employee";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<TicketStatus> ticketStatusIQ = from ts in _context.TicketStatus
                                                      select ts;

            if (!String.IsNullOrEmpty(searchString))
            {
                ticketStatusIQ = ticketStatusIQ.Where(ts =>
                        ts.StatusName.Contains(searchString)
                        || ts.Ticket.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "StatusName_desc":
                    ticketStatusIQ = ticketStatusIQ.OrderByDescending(t => t.StatusName);
                    break;
                case "StatusDate":
                    ticketStatusIQ = ticketStatusIQ.OrderBy(t => t.StatusDate);
                    break;
                case "StatusDate_desc":
                    ticketStatusIQ = ticketStatusIQ.OrderByDescending(t => t.StatusDate);
                    break;
                case "Employee":
                    ticketStatusIQ = ticketStatusIQ.OrderBy(t => t.Employee);
                    break;
                case "Employee_desc":
                    ticketStatusIQ = ticketStatusIQ.OrderByDescending(t => t.Employee);
                    break;
                case "ModifDate17118162":
                    ticketStatusIQ = ticketStatusIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    ticketStatusIQ = ticketStatusIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    ticketStatusIQ = ticketStatusIQ.OrderBy(t => t.StatusName);
                    break;
            }

            int pageSize = 5;

            TicketStatus = await PaginatedList<TicketStatus>
                .CreateAsynx(ticketStatusIQ
                .Include(t => t.Employee)
                .Include(t => t.Ticket)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
