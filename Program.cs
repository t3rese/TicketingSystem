using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Xml;
using static System.Environment;

namespace TicketingSystem
{
    class Program
    {
        private const int READ = 1;
        private const int WRITE = 2;
        private const int EXIT = 3;

        //Menu method 
        public static int GetMenuChoice()
        {
            Console.WriteLine("\nTicketing System");
            Console.WriteLine("++++++++++++++++");
            Console.WriteLine("1. View Open Tickets");
            Console.WriteLine("2. Open Ticket");
            Console.WriteLine("3. Exit\n");
            Console.Write("Select an option (Enter 1, 2 or 3): ");

            int menuChoice = Convert.ToInt32(Console.ReadLine());
            return menuChoice;
        }

        static void Main(string[] args)
        {
            //get menu choice from user 
            int option = GetMenuChoice();

            while (option != EXIT)
            {
                switch (option)
                {
                    case READ:
                        ReadFile();
                        break;

                    case WRITE:
                        WriteToFile();
                        break;
                }

                option = GetMenuChoice();
            }
        }

        //write to file 
        private static void WriteToFile()
        {
            string file = "Files/tickets.csv";
            StreamWriter sw = new StreamWriter(file, true);

            //prompt user for data and read input 
            Console.Write("Enter Ticket ID: ");
            int ticketID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter summary: ");
            string summary = Console.ReadLine();

            Console.Write("Enter status (open/closed): ");
            string status = Console.ReadLine();

            Console.Write("Enter priority (low, medium, high): ");
            string priority = Console.ReadLine();

            Console.Write("Enter submitter name: ");
            string submitter = Console.ReadLine();

            Console.Write("Enter name of assigned tech: ");
            string assignedTo = Console.ReadLine();

            Console.Write("How many are watching?: ");
            int watchingQty = Convert.ToInt32(Console.ReadLine());

            //string[] watchingUsers = new string[watchingQty];
            var watchingList = new List<string>();

            for (int i = 1; i <= watchingQty; i++)
            {
                Console.Write("Enter name of watching: ");
                string watcher = Console.ReadLine();
                watchingList.Add(watcher);
            }

            string allWatchers = null;

            foreach (var watcher in watchingList)
            {
                allWatchers = string.Join('|', watchingList);
            }


            //write user input to a file 
            var newEntry = string.Join(',', ticketID, summary, status, priority, submitter, assignedTo, allWatchers);
            sw.WriteLine(newEntry, true);
            sw.Close();
        }

        //Read file 
        private static void ReadFile()
        {
            string file = "Files/tickets.csv";
            if (File.Exists(file))
            {
                //skip header row
                StreamReader sr = new StreamReader(file);
                string firstLine = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] column = line.Split(',');
                    Console.WriteLine(
                        "\nTicket ID: {0} \nSummary: {1} \nStatus: {2} \nPriority: {3} \nSubmitter: {4} \nAssigned: {5} \nWatching: {6}",
                        column[0], column[1], column[2], column[3], column[4], column[5], column[6]);
                    Console.WriteLine();
                }

                sr.Close();
            }
            else
            {
                Console.WriteLine("No file exists");
            }
        }
    }
}
