using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class TicketStatus : BaseEntity
    {
        public Guid TicketStatusID { get; set; }

        public string StatusName { get; set; }

        public Guid TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }

        public DateTime StatusDate { get; set; } = DateTime.Now;

        public Guid EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
