using System;
using System.Collections.Generic;

namespace TicketConsole
{
    public class TicketManager
    {
        public List<Ticket> Tickets = new List<Ticket>();
        public FileOperations FileOperations = new FileOperations();

        private Dictionary<string, int> ticketCounts = new Dictionary<string, int>
        {
            ["Bug"] = 0,
            ["Enhancement"] = 0,
            ["Task"] = 0
        };
        
        private Dictionary<string, List<Ticket>> TicketLists = new Dictionary<string, List<Ticket>>
        {
            ["Bug"] = new List<Ticket>(),
            ["Enhancement"] = new List<Ticket>(),
            ["Task"] = new List<Ticket>()
        };

        public List<List<Ticket>> GetTicketLists()
        {
            List<List<Ticket>> ticketLists = new List<List<Ticket>>();
            foreach (var list in TicketLists)
            {
                if (list.Value.Count > 0)
                {
                    ticketLists.Add(list.Value);
                }
            }

            return ticketLists;
        }

        public Ticket CreateNewTicket(Dictionary<string, string> ticketInfo)
        {
            Ticket ticket;
            switch (ticketInfo["Type"])
            {
                case "Bug":
                    ticket = CreateBugTicket(ticketInfo);
                    break;
                case "Enhancement":
                    ticket = CreateEnhancementTicket(ticketInfo);
                    break;
                case "Task":
                    ticket = CreateTaskTicket(ticketInfo);
                    break;
                default:
                    ticket = CreateBugTicket(ticketInfo);
                    break;
            }
            TicketLists[ticketInfo["Type"]].Add(ticket);
            return ticket;
        }

        private Ticket AssignGeneralTicketInfo(Ticket ticket, Dictionary<string, string> ticketInfo)
        {
            ticket.Summary = ticketInfo["Summary"];
            ticket.Status = ticketInfo["Status"];
            ticket.Priority = ticketInfo["Priority"];
            ticket.Submitter = ticketInfo["Submitter"];
            ticket.Assigned = ticketInfo["Assigned"];
            ticket.Watching = ticketInfo["Watching"];
            
            if (ticketInfo.TryGetValue("TicketID", out var result))
            {
                ticket.TicketID = int.Parse(result);
                ticketCounts[ticketInfo["Type"]]++;
            }
            else
            {
                ticket.TicketID = ++ticketCounts[ticketInfo["Type"]];
            }

            return ticket;
        }

        private Ticket CreateBugTicket(Dictionary<string, string> ticketInfo)
        {
            Ticket newTicket = new BugTicket()
            {
                Severity = ticketInfo["Severity"]
            };

            return AssignGeneralTicketInfo(newTicket, ticketInfo);
        }

        private Ticket CreateEnhancementTicket(Dictionary<string, string> ticketInfo)
        {
            Ticket newTicket = new EnhancementTicket()
            {
                Software = ticketInfo["Software"],
                Cost = int.Parse(ticketInfo["Cost"]),
                Reason = ticketInfo["Reason"],
                Estimate = ticketInfo["Estimate"]
            };
            return AssignGeneralTicketInfo(newTicket, ticketInfo);
        }

        private Ticket CreateTaskTicket(Dictionary<string, string> ticketInfo)
        {
            Ticket newTicket = new TaskTicket()
            {
                ProjectName = ticketInfo["ProjectName"],
                DueDate = DateTime.Parse(ticketInfo["DueDate"])
            };
            return AssignGeneralTicketInfo(newTicket, ticketInfo);
        }

        public void ReadAllTickets()
        {
            var allTickets = new List<Dictionary<string, string>>();
            allTickets.AddRange(ReadBugTickets());
            allTickets.AddRange(ReadEnhancementTickets());
            allTickets.AddRange(ReadTaskTickets());

            foreach (var ticketInfo in allTickets)
            {
                var newTicket = CreateNewTicket(ticketInfo);
                Tickets.Add(newTicket);
            }
            
        }

        private List<Dictionary<string, string>> ReadBugTickets()
        {
            var tickets = FileOperations.ReadTickets(TicketType.Bug);
            foreach (var ticket in tickets)
            {
                ticket["Type"] = TicketType.Bug.ToString();
            }

            return tickets;
        }
        
        private List<Dictionary<string, string>> ReadEnhancementTickets()
        {
            var tickets = FileOperations.ReadTickets(TicketType.Enhancement);
            foreach (var ticket in tickets)
            {
                ticket["Type"] = TicketType.Enhancement.ToString();
            }

            return tickets;
        }
        
        private List<Dictionary<string, string>> ReadTaskTickets()
        {
            var tickets = FileOperations.ReadTickets(TicketType.Task);
            foreach (var ticket in tickets)
            {
                ticket["Type"] = TicketType.Task.ToString();
            }

            return tickets;
        }
    }
}