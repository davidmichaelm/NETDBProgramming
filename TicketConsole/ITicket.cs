namespace TicketConsole
{
    public interface ITicket
    {
        
        int Id { get; set; }

        string Summary { get; set; }

        string Status { get; set; }

        string Priority { get; set; }

        string Submitter { get; set; }

        string Assigned { get; set; }

        string Watching { get; set; }
    }
}