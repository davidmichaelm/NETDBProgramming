namespace TicketConsole
{
    class Ticket : ITicket
    {
        private static int _ticketCount;

        public Ticket(string id, string summary, string status, string priority, string submitter, string assigned, string watching)
        {
            if (id == null)
            {
                Id = ++_ticketCount;
            }
            else
            {
                Id = int.Parse(id);
                _ticketCount++;
            }
                
            Summary = summary;
            Status = status;
            Priority = priority;
            Submitter = submitter;
            Assigned = assigned;
            Watching = watching;
        }

        public int Id { get; set; }

        public string Summary { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        public string Submitter { get; set; }

        public string Assigned { get; set; }

        public string Watching { get; set; }

        public override string ToString()
        {
            return $"{Id},{Summary},{Status},{Priority},{Submitter},{Assigned},{Watching}";
        }
    }
}