using System;

namespace TicketConsole
{
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
}