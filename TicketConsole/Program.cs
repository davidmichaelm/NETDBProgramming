/*
    Complete and submit first phase of project
    - Build data file with initial system tickets and data in a CSV
    - Write Console application to process and add records to the CSV file

    Tickets.csv
    TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
    1,This is a bug ticket,Open,High,Drew Kjell,Jane Doe,Drew Kjell|John Smith|Bill Jones
*/

using System;
using System.Collections.Generic;

namespace TicketConsole
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
      
        public static void Main(string[] args)
        {
            new Program();
        }
        
        private UserInput _input = new UserInput();
        private List<Ticket> _tickets = new List<Ticket>();
        private FileOperations _fo = new FileOperations();

        public Program()
        {
            // open file and create existing tickets
            List<string[]> ticketArrays = _fo.ReadTickets();

            foreach (var ticket in ticketArrays)
            {
                _tickets.Add(new Ticket(ticket[0], ticket[1], ticket[2], ticket[3], ticket[4], ticket[5], ticket[6]));
            }
            
            logger.Info($"Loaded {_tickets.Count} tickets");

            // Let the user decide what to do
            var keepRunning = true;
            while (keepRunning)
            {
                switch (_input.GetMenuOption())
                {
                    case "1":
                        Console.WriteLine($"{"ID", -5}{"Summary", -25}{"Status", -15}{"Priority", -15}{"Submitter", -15}{"Assigned", -15}{"Watching", -15}");
                        foreach (var ticket in _tickets)
                        {
                            Console.WriteLine($"{ticket.Id, -5}{ticket.Summary, -25}{ticket.Status, -15}{ticket.Priority, -15}{ticket.Submitter, -15}{ticket.Assigned, -15}{ticket.Watching, -15}");
                        }
                        break;
                    case "2":
                        string summary = _input.GetTicketSummary();
                        string status = _input.GetTicketStatus();
                        string priority = _input.GetTicketPriority();
                        string submitter = _input.GetTicketSubmitter();
                        string assigned = _input.GetTicketAssigned();
                        string watching = _input.GetTicketWatching();

                        Ticket newTicket = new Ticket(null, summary, status, priority, submitter, assigned, watching);
            
                        _tickets.Add(newTicket);
                        _fo.AppendTicket(newTicket);
                        break;
                    default:
                        keepRunning = false;
                        break;
                }
            }
        }
    }
}
