/*
    Complete and submit first phase of project
    - Build data file with initial system tickets and data in a CSV
    - Write Console application to process and add records to the CSV file

    Tickets.csv
    TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
    1,This is a bug ticket,Open,High,Drew Kjell,Jane Doe,Drew Kjell|John Smith|Bill Jones
*/

namespace TicketConsole
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
      
        public static void Main(string[] args)
        {
            new Program();
        }
        
        private UserInput _ui = new UserInput();
        private TicketManager _ticketManager = new TicketManager();

        public Program()
        {
            _ticketManager.ReadAllTickets();
            logger.Info($"Loaded {_ticketManager.Tickets.Count} tickets");

            // Let the user decide what to do
            var keepRunning = true;
            while (keepRunning)
            {
                switch (_ui.GetMenuOption())
                {
                    case "1":
                        _ui.DisplayTickets(_ticketManager.GetTicketLists());
                        break;
                    case "2":
                        var newTicketInfo = _ui.GetNewTicket();
                        var newTicket = _ticketManager.CreateNewTicket(newTicketInfo);
                        _ticketManager.FileOperations.AppendTicket(newTicket);
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            }
        }
    }
}
