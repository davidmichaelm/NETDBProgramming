using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TicketConsole.Tickets;

namespace TicketConsole
{
    public class TicketManager
    {
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
            foreach (TicketType ticketType in Enum.GetValues(typeof(TicketType)))
            {
                var tickets = FileOperations.ReadTickets(ticketType);
                foreach (var ticket in tickets)
                {
                    ticket["Type"] = ticketType.ToString();
                }
                allTickets.AddRange(tickets);
            }
            
            foreach (var ticketInfo in allTickets)
            {
                CreateNewTicket(ticketInfo);
            }
        }

        public List<List<Ticket>> SearchTickets(string searchType, string searchQuery)
        {
            List<List<Ticket>> resultLists = new List<List<Ticket>>();
            foreach (var ticketList in GetTicketLists())
            {
                switch (searchType)
                {
                    case "Status":
                        resultLists.Add(ticketList.Where(t => t.Status.Contains(searchQuery)).ToList());
                        break;
                    case "Priority":
                        resultLists.Add(ticketList.Where(t => t.Priority.Contains(searchQuery)).ToList());
                        break;
                    case "Submitter":
                    default:
                        resultLists.Add(ticketList.Where(t => t.Submitter.Contains(searchQuery)).ToList());
                        break;
                }
            }

            return resultLists;
        }
        
        
    }
}
