using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Areas;
using TicketSystem.Models;

namespace TicketSystem.Pages.TicketCategories
{
    [Authorize(Roles = TicketSystemRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<TicketCategory> TicketCategory { get;set; }

        public string CategoryNameSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentFilter = searchString;
            CurrentSort = sortOrder;

            CategoryNameSort = sortOrder == "CategoryName" ? "CategoryName_desc" : "CategoryName";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<TicketCategory> ticketCategoryIQ = from tc in _context.TicketCategory
                                                 select tc;

            if (!String.IsNullOrEmpty(searchString))
            {
                ticketCategoryIQ = ticketCategoryIQ.Where(tc =>
                        tc.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "CategoryName_desc":
                    ticketCategoryIQ = ticketCategoryIQ.OrderByDescending(e => e.CategoryName);
                    break;
                case "ModifDate17118162":
                    ticketCategoryIQ = ticketCategoryIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    ticketCategoryIQ = ticketCategoryIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    ticketCategoryIQ = ticketCategoryIQ.OrderBy(e => e.CategoryName);
                    break;
            }

            int pageSize = 5;

            TicketCategory = await PaginatedList<TicketCategory>
                .CreateAsynx(ticketCategoryIQ
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
