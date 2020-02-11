using System;
using System.Collections.Generic;
using System.IO;

namespace TicketConsole
{
    class FileOperations
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private string _file = "Tickets.csv";
            
        // returns a List with an array of strings, each string corresponding with the ticket fields
        public List<string[]> ReadTickets()
        {
            List<string[]> tickets = new List<string[]>();

            try
            {
                using (StreamReader sr = new StreamReader(_file))
                {
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
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Reading tickets file failed");
            }
            
            return tickets;
        }

        public void AppendTicket(Ticket ticket)
        {
            try
            {
                using (var sw = new StreamWriter(_file, true))
                {
                    sw.WriteLine(ticket.ToString());
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Writing to tickets file failed");
            }
        }
    }
}