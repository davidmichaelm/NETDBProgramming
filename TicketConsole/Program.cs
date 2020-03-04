/*
    User should be able to perform a search based on status, priority or submitter
    The search results should display the results and the number of matches
    One single search should be return results from all ticket types (Concatenation Operator)
*/

using System;

namespace TicketConsole
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
      
        public static void Main(string[] args)
        {
            new Program();
        }
        
        private UserInterface _ui = new UserInterface();
        private TicketManager _ticketManager = new TicketManager();

        public Program()
        {
            _ticketManager.ReadAllTickets();
            
            
            logger.Info($"Loaded {_ticketManager.GetTotalTickets()} tickets");

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
                    case "3":
                        var searchType = _ui.GetSearchType();
                        var searchQuery = _ui.GetSearchQuery();
                        _ui.DisplaySearchResults(_ticketManager.SearchTickets(searchType, searchQuery));
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            }
        }
    }
}
