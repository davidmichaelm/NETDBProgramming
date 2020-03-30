using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketConsole.Tickets;

namespace TicketConsole
{
    class UserInterface
    {
        public string GetMenuOption()
        {
            Console.WriteLine("1) Read current tickets.");
            Console.WriteLine("2) Add new tickets to the file.");
            Console.WriteLine("3) Search tickets.");
            Console.WriteLine("Enter any other key to exit.");
            return Console.ReadLine();
        }
        
        public void DisplayTickets(List<List<Ticket>> tickets)
        {
            foreach (var list in tickets)
            {
                if (list.Count == 0)
                {
                    continue;
                }
                // Write headers
                Console.WriteLine(list[0].Type + " Tickets");
                var headers = list[0].GetProperties();
                foreach (var prop in headers)
                {
                    Console.Write(FormatProperty(prop, prop));
                }
                Console.WriteLine();

                // Write ticket values
                foreach (var ticket in list)
                {
                    Console.WriteLine(ticket.Display());
                }
                Console.WriteLine("\n");
            }
            
        }

        private string FormatProperty(string prop, string text)
        {
            string formattedProp;
            switch (prop)
            {
                case "TicketID":
                    formattedProp = $"{text, -10}";
                    break;
                case "Summary":
                    formattedProp = $"{text, -25}";
                    break;
                default:
                    formattedProp = $"{text, -15}";
                    break;
            }

            return formattedProp;
        }

        public Dictionary<string, string> GetNewTicket()
        {
            var ticketType = GetTicketType();
            var ticketInfo = GetTicketInfo();
            ticketInfo["Type"] = ticketType.ToString();
            
            Dictionary<string, string> ticketTypeInfo = new Dictionary<string, string>();
            switch (ticketType)
            {
                case TicketType.Bug:
                    ticketTypeInfo = GetBugTicketInfo();
                    break;
                case TicketType.Enhancement:
                    ticketTypeInfo = GetEnhancementTicketInfo();
                    break;
                case TicketType.Task:
                    ticketTypeInfo = GetTaskTicketInfo();
                    break;
            }

            foreach (var info in ticketTypeInfo)
            {
                ticketInfo.Add(info.Key, info.Value);
            }

            return ticketInfo;
        }

        private TicketType GetTicketType()
        {
            Console.WriteLine("Enter ticket type:");
            Console.WriteLine("1) Bug");
            Console.WriteLine("2) Enhancement");
            Console.WriteLine("3) Task");

            bool keepAsking = true;
            TicketType ticketType = TicketType.Bug;
            while (keepAsking)
            {
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ticketType = TicketType.Bug;
                        keepAsking = false;
                        break;
                    case "2":
                        ticketType = TicketType.Enhancement;
                        keepAsking = false;
                        break;
                    case "3":
                        ticketType = TicketType.Task;
                        keepAsking = false;
                        break;
                    default:
                        Console.WriteLine("Enter 1, 2, or 3.");
                        break;
                }
            }

            return ticketType;
        }

        private Dictionary<string, string> GetTicketInfo()
        {
            var ticketInfo = new Dictionary<string, string>
            {
                ["Summary"] = GetTicketSummary(),
                ["Status"] = GetTicketStatus(),
                ["Priority"] = GetTicketPriority(),
                ["Submitter"] = GetTicketSubmitter(),
                ["Assigned"] = GetTicketAssigned(),
                ["Watching"] = GetTicketWatching()
            };
            return ticketInfo;
        }

        private Dictionary<string, string> GetBugTicketInfo()
        {
            var ticketInfo = new Dictionary<string, string>
            {
                ["Severity"] = GetTicketSeverity()
            };
            return ticketInfo;
        }

        private Dictionary<string, string> GetEnhancementTicketInfo()
        {
            var ticketInfo = new Dictionary<string, string>
            {
                ["Software"] = GetTicketSoftware(),
                ["Cost"] = GetTicketCost(),
                ["Reason"] = GetTicketReason(),
                ["Estimate"] = GetTicketEstimate()
            };
            return ticketInfo;
        }

        private Dictionary<string, string> GetTaskTicketInfo()
        {
            var ticketInfo = new Dictionary<string, string>()
            {
                ["ProjectName"] = GetTicketProjectName(),
                ["DueDate"] = GetTicketDueDate()
            };
            return ticketInfo;
        }

        private string GetTicketSummary()
        {
            Console.WriteLine("Enter the ticket summary:");
            return Console.ReadLine();
        }

        private string GetTicketStatus()
        {
            Console.WriteLine("Enter the ticket status (open/closed):");
            return Console.ReadLine();
        }

        private string GetTicketPriority()
        {
            Console.WriteLine("Enter the ticket priority (low, medium, high):");
            return Console.ReadLine();
        }

        private string GetTicketSubmitter()
        {
            Console.WriteLine("Enter the person who submitted the ticket:");
            return Console.ReadLine();
        }

        private string GetTicketAssigned()
        {
            Console.WriteLine("Enter the person assigned to the ticket:");
            return Console.ReadLine();
        }

        private string GetTicketWatching()
        {
            Console.WriteLine("Enter the person watching the ticket:");
            Console.WriteLine("(If multiple people are watching, separate names with the | symbol)");
            return Console.ReadLine();
        }

        private string GetTicketSeverity()
        {
            Console.WriteLine("Enter the severity of the ticket:");
            return Console.ReadLine();
        }

        private string GetTicketSoftware()
        {
            Console.WriteLine("Enter the software to enhance:");
            return Console.ReadLine();
        }

        private string GetTicketCost()
        {
            Console.WriteLine("Enter the estimated cost:");
            var cost = Console.ReadLine();
            
            while (int.TryParse(cost, out var intCost) == false)
            {
                Console.WriteLine("Error parsing number. Please enter a number.");
                Console.WriteLine("Enter the estimated cost:");
                cost = Console.ReadLine();
            }

            return cost;
        }

        private string GetTicketReason()
        {
            Console.WriteLine("Enter the reason for the enhancement request:");
            return Console.ReadLine();
        }

        private string GetTicketEstimate()
        {
            Console.WriteLine("Enter the ticket estimate:");
            return Console.ReadLine();
        }

        private string GetTicketProjectName()
        {
            Console.WriteLine("Enter the project name:");
            return Console.ReadLine();
        }

        private string GetTicketDueDate()
        {
            Console.WriteLine("Enter the ticket due date:");
            var dueDate = Console.ReadLine();

            while (DateTime.TryParse(dueDate, out var result) == false)
            {
                Console.WriteLine("Error parsing date. Please enter a date format (mm/dd/yyyy)");
                Console.WriteLine("Enter the ticket due date:");
                dueDate = Console.ReadLine();
            }

            return dueDate;
        }

        public string GetSearchQuery()
        {
            Console.WriteLine("Enter search query:");
            var input = Console.ReadLine();
            while (input == null || input.Equals(""))
            {
                input = Console.ReadLine();
            }
            return input;
        }

        public string GetSearchType()
        {
            Console.WriteLine("1) Search in status");
            Console.WriteLine("2) Search in priorities");
            Console.WriteLine("3) Search in submitters");

            var input = Console.ReadLine();
            while (!new [] {"1", "2", "3"}.Contains(input))
            {
                input = Console.ReadLine();
            }
            
            switch (input)
            {
                case "1":
                    return "Status";
                case "2":
                    return "Priority";
                case "3":
                default:
                    return "Submitter";
            }
        }

        public void DisplaySearchResults(List<List<Ticket>> results)
        {
            Console.WriteLine($"Total results: {GetResultsCount(results)}");
            DisplayTickets(results);
        }

        private int GetResultsCount(List<List<Ticket>> results)
        {
            var counter = 0;
            foreach (var list in results)
            {
                counter += list.Count;
            }

            return counter;
        }
    }
}