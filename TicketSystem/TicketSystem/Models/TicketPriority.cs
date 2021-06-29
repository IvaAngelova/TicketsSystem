using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Models
{
    [Index(nameof(PriorityType), IsUnique = true)]
    public class TicketPriority : BaseEntity
    {
        public TicketPriority()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public Guid TicketPriorityID { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Priority is required")]
        public string PriorityType { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
