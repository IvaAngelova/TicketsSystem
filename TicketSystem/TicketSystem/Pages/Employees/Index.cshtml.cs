using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.Employees
{
    [Authorize(Roles = TicketSystemRoles.AdminOrManagerOrEmp)]
    public class IndexModel : PageModel
    {
        private readonly TicketSystemContext _context;

        public IndexModel(TicketSystemContext context)
        {
            _context = context;
        }

        public PaginatedList<Employee> Employee { get; set; }
        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }
        public string DepartmentSort { get; set; }
        public string ModifDate17118162Sort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString,
            string currentFilter, int? pageIndex)
        {
            CurrentFilter = searchString;
            CurrentSort = sortOrder;

            FirstNameSort = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            LastNameSort = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            DepartmentSort = sortOrder == "Department" ? "Department_desc" : "Department";
            ModifDate17118162Sort = sortOrder == "ModifDate17118162" ? "ModifDate17118162_desc" : "ModifDate17118162";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Employee> employeeIQ = from e in _context.Employee
                                              select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                employeeIQ = employeeIQ.Where(e =>
                        e.FirstName.Contains(searchString)
                        || e.LastName.Contains(searchString)
                        || e.Department.DepartmentName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "FirstName_desc":
                    employeeIQ = employeeIQ.OrderByDescending(e => e.FirstName);
                    break;
                case "LastName":
                    employeeIQ = employeeIQ.OrderBy(e => e.LastName);
                    break;
                case "LastName_desc":
                    employeeIQ = employeeIQ.OrderByDescending(e => e.LastName);
                    break;
                case "Department":
                    employeeIQ = employeeIQ.OrderBy(e => e.Department);
                    break;
                case "Department_desc":
                    employeeIQ = employeeIQ.OrderByDescending(e => e.Department);
                    break;
                case "ModifDate17118162":
                    employeeIQ = employeeIQ.OrderBy(e => e.ModifDate17118162);
                    break;
                case "ModifDate17118162_desc":
                    employeeIQ = employeeIQ.OrderByDescending(e => e.ModifDate17118162);
                    break;
                default:
                    employeeIQ = employeeIQ.OrderBy(e => e.FirstName);
                    break;
            }

            int pageSize = 5;

            Employee = await PaginatedList<Employee>
                .CreateAsynx(employeeIQ
                .Include(e => e.Department)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
