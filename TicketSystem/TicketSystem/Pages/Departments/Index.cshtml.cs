using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Areas;
using TicketSystem.Models;

namespace TicketSystem.Pages.Departments
{
    [Authorize(Roles = TicketSystemRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<Department> Department { get; set; }

        public string DepartmentNameSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentFilter = searchString;
            CurrentSort = sortOrder;

            DepartmentNameSort = sortOrder == " DepartmentName" ? " DepartmentName_desc" : " DepartmentName";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Department> deparmentIQ = from d in _context.Department
                                                 select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                deparmentIQ = deparmentIQ.Where(a =>
                        a.DepartmentName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TypeName_desc":
                    deparmentIQ = deparmentIQ.OrderByDescending(e => e.DepartmentName);
                    break;
                case "ModifDate17118162":
                    deparmentIQ = deparmentIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    deparmentIQ = deparmentIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    deparmentIQ = deparmentIQ.OrderBy(e => e.DepartmentName);
                    break;
            }

            int pageSize = 5;

            Department = await PaginatedList<Department>
                .CreateAsynx(deparmentIQ
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
