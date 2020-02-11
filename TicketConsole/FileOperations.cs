using System.Collections.Generic;
using System.IO;

namespace TicketConsole
{
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