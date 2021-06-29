using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Models 
{
    [Index(nameof(DepartmentName), IsUnique = true)]
    public class Department : BaseEntity
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public Guid DepartmentID { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
