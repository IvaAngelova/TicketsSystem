using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class TicketCategory : BaseEntity
    {
        public TicketCategory()
        {
            this.Employees = new HashSet<Employee>();
            this.Tickets = new HashSet<Ticket>();
        }

        public Guid TicketCategoryID { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
