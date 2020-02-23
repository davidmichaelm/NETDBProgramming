using System.Collections.Generic;

namespace TicketConsole
{
    public class EnhancementTicket : Ticket
    {
        public override TicketType Type => TicketType.Enhancement;
        public string Software { get; set; }
        
        public double Cost { get; set; }
        
        public string Reason { get; set; }
        
        public string Estimate { get; set; }

        public override List<string> GetProperties()
        {
            var props = base.GetProperties();
            props.Add("Software");
            props.Add("Cost");
            props.Add("Reason");
            props.Add("Estimate");
            return props;
        }

        public override string Display()
        {
            return base.Display() + $"{Software, -15}{Cost, -15}{Reason, -15}{Estimate, -15}";
        }

        public override string ToString()
        {
            return base.ToString() + $",{Software},{Cost},{Reason},{Estimate}";
        }
    }
}