using System;
using System.Collections.Generic;
using System.IO;
using TicketConsole.Tickets;

namespace TicketConsole
{
    public class FileOperations
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private Dictionary<string, string> _files = new Dictionary<string, string>
        {
            ["Bug"] = "Spreadsheets/Tickets.csv",
            ["Enhancement"] = "Spreadsheets/Enhancements.csv",
            ["Task"] = "Spreadsheets/Task.csv"
        };

        // returns a List with an array of strings, each string corresponding with the ticket fields
        public List<Dictionary<string, string>> ReadTickets(TicketType type)
        {
            var tickets = new List<Dictionary<string, string>>();

            try
            {
                using (StreamReader sr = new StreamReader(_files[type.ToString()]))
                {
                    int counter = 0;
                    var headers = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        
                        if (line != null)
                        {
                            string[] lineArray = line.Split(',');
                            if (counter == 0) // Add the headers to a separate list
                            {
                                headers.AddRange(lineArray);
                            }
                            else
                            {
                                var ticket = new Dictionary<string, string>();
                                for (int i = 0; i < lineArray.Length; i++)
                                {
                                    ticket.Add(headers[i], lineArray[i]);
                                }
                                tickets.Add(ticket);
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
                using (var sw = new StreamWriter(_files[ticket.Type.ToString()], true))
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