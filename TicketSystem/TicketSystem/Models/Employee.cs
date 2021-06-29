using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            this.CreatorTickets = new HashSet<Ticket>();
            this.АcceptedАТickets = new HashSet<Ticket>();
            this.TicketStatuses = new HashSet<TicketStatus>();
        }

        public Guid EmployeeID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [StringLength(35)]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [MinLength(15)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Address is required")]
        public string Email { get; set; }     

        public Guid DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Ticket> CreatorTickets { get; set; }

        public virtual ICollection<Ticket> АcceptedАТickets { get; set; }

        public virtual ICollection<TicketStatus> TicketStatuses { get; set; }

        public string UserId { get; set; }
    }
}
