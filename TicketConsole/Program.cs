/*
    Complete and submit first phase of project
    - Build data file with initial system tickets and data in a CSV
    - Write Console application to process and add records to the CSV file

    Tickets.csv
    TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
    1,This is a bug ticket,Open,High,Drew Kjell,Jane Doe,Drew Kjell|John Smith|Bill Jones
    
    StreamWriter append = true
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
        private UserInput input = new UserInput();
        private List<Ticket> tickets = new List<Ticket>();
        private FileReader fr = new FileReader();

        public void Run()
        {
            // open file and create existing tickets
            List<string[]> ticketArrays = fr.ReadTickets();

            foreach (var t in ticketArrays)
            {
                tickets.Add(new Ticket(t[0], t[1], t[2], t[3], t[4], t[5]));
            }

            // Let the user decide what to do
            switch (input.getMenuOption())
            {
                case "1":
                    // read file
                    break;
                case "2":
                    string summary = input.getTicketSummary();
                    string status = input.getTicketStatus();
                    string priority = input.getTicketPriority();
                    string submitter = input.getTicketSubmitter();
                    string assigned = input.getTicketAssigned();
                    string watching = input.getTicketWatching();
            
                    tickets.Add(new Ticket(summary, status, priority, submitter, assigned, watching));
                    break;
            }
        }
    }

    class UserInput
        {
            public string getMenuOption()
            {
                Console.WriteLine("1) Read tickets from the file.");
                Console.WriteLine("2) Add new tickets to the file.");
                Console.WriteLine("Enter any other key to exit.");
                return Console.ReadLine();
            }

            public string getTicketSummary()
            {
                Console.WriteLine("Enter the ticket summary:");
                return Console.ReadLine();
            }

            public string getTicketStatus()
            {
                Console.WriteLine("Enter the ticket status (open/closed):");
                return Console.ReadLine();
            }

            public string getTicketPriority()
            {
                Console.WriteLine("Enter the ticket priority (low, medium, high):");
                return Console.ReadLine();
            }

            public string getTicketSubmitter()
            {
                Console.WriteLine("Enter the person who submitted the ticket:");
                return Console.ReadLine();
            }

            public string getTicketAssigned()
            {
                Console.WriteLine("Enter the person assigned to the ticket:");
                return Console.ReadLine();
            }

            public string getTicketWatching()
            {
                Console.WriteLine("Enter the person watching the ticket:");
                Console.WriteLine("(If multiple people are watching, separate names with the | symbol)");
                return Console.ReadLine();
            }
        }

        class Ticket
        {
            private static int _ticketCount;
            private int _id;
            private string _summary;
            private string _status;
            private string _priority;
            private string _submitter;
            private string _assigned;
            private string _watching;

            public Ticket(string summary, string status, string priority, string submitter, string assigned, string watching)
            {
                _id = _ticketCount++;
                _summary = summary;
                _status = status;
                _priority = priority;
                _submitter = submitter;
                _assigned = assigned;
                _watching = watching;
            }

            public int Id
            {
                get => _id;
                set => _id = value;
            }

            public string Summary
            {
                get => _summary;
                set => _summary = value;
            }

            public string Status
            {
                get => _status;
                set => _status = value;
            }

            public string Priority
            {
                get => _priority;
                set => _priority = value;
            }

            public string Submitter
            {
                get => _submitter;
                set => _submitter = value;
            }

            public string Assigned
            {
                get => _assigned;
                set => _assigned = value;
            }

            public string Watching
            {
                get => _watching;
                set => _watching = value;
            }

            public string GetOutput()
            {
                return $"{_summary},{_status},{_priority},{_submitter},{_assigned},{_watching}";
            }
        }

        class FileReader
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
                        if (counter > 1) // Ignore the headers
                        {
                            string[] lineArray = line.Split(',');
                            tickets.Add(lineArray);
                            counter++;
                        }
                    }
                    sr.Close();
                }
                return tickets;
            }
        }
    
}
