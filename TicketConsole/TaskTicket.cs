using System;
using System.Collections.Generic;

namespace TicketConsole
{
    public class TaskTicket : Ticket
    {
        public override TicketType Type => TicketType.Task;

        public string ProjectName { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public override List<string> GetProperties()
        {
            var props = base.GetProperties();
            props.Add("ProjectName");
            props.Add("DueDate");
            return props;
        }

        public override string Display()
        {
            return base.Display() + $"{ProjectName, -15}{DueDate, -15}";
        }

        public override string ToString()
        {
            return base.ToString() + $",{ProjectName, -15},{DueDate, -15}";
        }
    }
}