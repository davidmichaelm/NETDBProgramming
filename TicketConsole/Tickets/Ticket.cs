using System.Collections.Generic;

namespace TicketConsole.Tickets
{
    public abstract class Ticket : ITicket
    {
        public abstract TicketType Type { get; }

        public virtual List<string> GetProperties()
        {
            return new List<string>
            {
              "TicketID","Summary","Status","Priority","Submitter","Assigned","Watching"  
            };
        }

        public int TicketID { get; set; }

        public string Summary { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        public string Submitter { get; set; }

        public string Assigned { get; set; }

        public string Watching { get; set; }

        public virtual string Display()
        {
            return $"{TicketID, -10}{Summary, -25}{Status, -15}{Priority, -15}{Submitter, -15}{Assigned, -15}{Watching, -15}";

        }

        public override string ToString()
        {
            return $"{TicketID},{Summary},{Status},{Priority},{Submitter},{Assigned},{Watching}";
        }
    }
}