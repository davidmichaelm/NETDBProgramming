namespace TicketConsole.Tickets
{
    public interface ITicket
    {
        
        int TicketID { get; set; }

        string Summary { get; set; }

        string Status { get; set; }

        string Priority { get; set; }

        string Submitter { get; set; }

        string Assigned { get; set; }

        string Watching { get; set; }
    }
}