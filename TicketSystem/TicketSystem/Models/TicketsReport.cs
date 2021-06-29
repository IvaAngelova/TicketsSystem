using System;

namespace TicketSystem.Models
{
    public class TicketsReport
    {
        public string Title { get; set; }

        public string Creator { get; set; }

        public string Accepted { get; set; }

        public DateTime OpenDate { get; set; }

        public string TicketPriority { get; set; }
    }
}
