using System.Collections.Generic;

namespace TicketConsole.Tickets
{
    public class BugTicket : Ticket
    {
        public override TicketType Type => TicketType.Bug;
        public string Severity { get; set; }
        
        public override List<string> GetProperties()
        {
            var props = base.GetProperties();
            props.Add("Severity");
            return props;
        }

        public override string Display()
        {
            return base.Display() + $"{Severity, -15}";
        }

        public override string ToString()
        {
            return base.ToString() + $",{Severity}";
        }

        
    }
}