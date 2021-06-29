using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            this.TicketStatuses = new HashSet<TicketStatus>();
        }

        public Guid TicketID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [ForeignKey("Employee")]
        public Guid CreatorID { get; set; }
        public virtual Employee CreatorTicket { get; set; }

        [ForeignKey("Employee")]
        public Guid? AcceptedATicketID { get; set; }
        public virtual Employee AcceptedATicket { get; set; }

        public Guid TicketCategoryID { get; set; }
        public virtual TicketCategory TicketCategory { get; set; }

        public DateTime OpenDate { get; set; } = DateTime.Now;

        public virtual ICollection<TicketStatus> TicketStatuses { get; set; }

        public Guid TicketPriorityID { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }

        public byte[] Photo { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }
    }
}
