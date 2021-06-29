using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Areas;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketPriorities
{
    [Authorize(Roles = TicketSystemRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<TicketPriority> TicketPriority { get;set; }

        public string PriorityTypeSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentFilter = searchString;
            CurrentSort = sortOrder;

            PriorityTypeSort = sortOrder == "PriorityType" ? "PriorityType_desc" : "PriorityType";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<TicketPriority> ticketPriorityIQ = from tp in _context.TicketPriority
                                                 select tp;

            if (!String.IsNullOrEmpty(searchString))
            {
                ticketPriorityIQ = ticketPriorityIQ.Where(tp =>
                        tp.PriorityType.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "PriorityType_desc":
                    ticketPriorityIQ = ticketPriorityIQ.OrderByDescending(e => e.PriorityType);
                    break;
                case "ModifDate17118162":
                    ticketPriorityIQ = ticketPriorityIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    ticketPriorityIQ = ticketPriorityIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    ticketPriorityIQ = ticketPriorityIQ.OrderBy(e => e.PriorityType);
                    break;
            }

            int pageSize = 5;

            TicketPriority = await PaginatedList<TicketPriority>
                .CreateAsynx(ticketPriorityIQ
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
