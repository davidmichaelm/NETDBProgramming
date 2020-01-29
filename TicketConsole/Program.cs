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
using System.IO;

namespace TicketConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Driver driver = new Driver();
            driver.Run();
        }
    }

    class Driver
    {
        private UserInput _input;
        private List<Ticket> _tickets;
        private FileOperations _fo;

        public Driver()
        {
            _input = new UserInput();
            _tickets = new List<Ticket>();
            _fo = new FileOperations();
        }

        public void Run()
        {
            // open file and create existing tickets
            List<string[]> ticketArrays = _fo.ReadTickets();

            foreach (var ticket in ticketArrays)
            {
                _tickets.Add(new Ticket(ticket[0], ticket[1], ticket[2], ticket[3], ticket[4], ticket[5], ticket[6]));
            }

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

    class UserInput
        {
            public string GetMenuOption()
            {
                Console.WriteLine("1) Read current tickets.");
                Console.WriteLine("2) Add new tickets to the file.");
                Console.WriteLine("Enter any other key to exit.");
                return Console.ReadLine();
            }

            public string GetTicketSummary()
            {
                Console.WriteLine("Enter the ticket summary:");
                return Console.ReadLine();
            }

            public string GetTicketStatus()
            {
                Console.WriteLine("Enter the ticket status (open/closed):");
                return Console.ReadLine();
            }

            public string GetTicketPriority()
            {
                Console.WriteLine("Enter the ticket priority (low, medium, high):");
                return Console.ReadLine();
            }

            public string GetTicketSubmitter()
            {
                Console.WriteLine("Enter the person who submitted the ticket:");
                return Console.ReadLine();
            }

            public string GetTicketAssigned()
            {
                Console.WriteLine("Enter the person assigned to the ticket:");
                return Console.ReadLine();
            }

            public string GetTicketWatching()
            {
                Console.WriteLine("Enter the person watching the ticket:");
                Console.WriteLine("(If multiple people are watching, separate names with the | symbol)");
                return Console.ReadLine();
            }
        }

        class Ticket
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

        class FileOperations
        {
            private string _file = "Tickets.csv";
            
            // returns a List with an array of strings, each string corresponding with the ticket fields
            public List<string[]> ReadTickets()
            {
                List<string[]> tickets = new List<string[]>();
                if (File.Exists(_file))
                {
                    
                    StreamReader sr = new StreamReader(_file);

                    int counter = 0;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (counter > 0) // Ignore the headers
                        {
                            if (line != null)
                            {
                                string[] lineArray = line.Split(',');
                                tickets.Add(lineArray);
                            }
                        }
                        counter++;
                    }
                    sr.Close();
                }
                return tickets;
            }

            public void AppendTicket(Ticket ticket)
            {
                var sw = new StreamWriter(_file, true);
                
                sw.WriteLine(ticket.ToString());
                sw.Close();
            }
        }
    
}
