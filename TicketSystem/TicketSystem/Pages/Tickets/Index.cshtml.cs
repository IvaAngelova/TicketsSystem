using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    [Authorize(Roles = TicketSystemRoles.AdminOrManagerOrEmp)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<Ticket> Ticket { get; set; }

        public string TicketPrioritySort { get; set; }
        public string OpenDateSort { get; set; }
        public string CreatorTicketSort { get; set; }
        public string АcceptedАТicketSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentSort = sortOrder;
            CurrentFilter = searchString;

            TicketPrioritySort = sortOrder == "TicketPriority" ? "TicketPriority_desc" : "TicketPriority";
            OpenDateSort = sortOrder == "OpenDate" ? "OpenDate_desc" : "OpenDate";
            CreatorTicketSort = sortOrder == "CreatorTicket" ? "CreatorTicket_desc" : "CreatorTicket";
            АcceptedАТicketSort = sortOrder == "АcceptedАТicket" ? "АcceptedАТicket_desc" : "АcceptedАТicket";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Ticket> ticketIQ = from t in _context.Ticket
                                          select t;

            if (User.Identity.IsAuthenticated && User.IsInRole("Служител"))
            {
                var employeeId = _context.Employee.FirstOrDefault(e => e.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .EmployeeID;

                ticketIQ = ticketIQ.Where(t => t.CreatorID == employeeId);
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                ticketIQ = ticketIQ.Where(t =>
                        t.TicketPriority.PriorityType.Contains(searchString)
                        || t.CreatorTicket.FirstName.Contains(searchString)
                        || t.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TicketPriority_desc":
                    ticketIQ = ticketIQ.OrderByDescending(t => t.TicketPriority);
                    break;
                case "OpenDate":
                    ticketIQ = ticketIQ.OrderBy(t => t.OpenDate);
                    break;
                case "OpenDate_desc":
                    ticketIQ = ticketIQ.OrderByDescending(t => t.CreatorTicket);
                    break;
                case "CreatorTicket":
                    ticketIQ = ticketIQ.OrderBy(t => t.CreatorTicket);
                    break;
                case "CreatorTicket_desc":
                    ticketIQ = ticketIQ.OrderByDescending(t => t.OpenDate);
                    break;
                case "АcceptedАТicket":
                    ticketIQ = ticketIQ.OrderBy(t => t.AcceptedATicket);
                    break;
                case "АcceptedАТicket_desc":
                    ticketIQ = ticketIQ.OrderByDescending(t => t.AcceptedATicket);
                    break;
                case "ModifDate17118162":
                    ticketIQ = ticketIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    ticketIQ = ticketIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    ticketIQ = ticketIQ.OrderBy(t => t.TicketPriority);
                    break;
            }

            int pageSize = 5;

            Ticket = await PaginatedList<Ticket>
                .CreateAsynx(ticketIQ
                .Include(t => t.CreatorTicket)
                .Include(t => t.TicketCategory)
                .Include(t => t.TicketPriority)
                .Include(t => t.AcceptedATicket)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
