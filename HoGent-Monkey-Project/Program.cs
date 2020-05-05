using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HoGent_Monkey_Project
{
    class Program
    {
        public static String ExportPath = "";

        public static DatabaseUtil db;
        public static String mysql_host = "timdesmet.be";
        public static String mysql_user = "u32002p26917_hogent";
        public static String mysql_pass;
        public static String mysql_data = "u32002p26917_hogent";

        public static String[] monkeyNames = { "Jan", "Fred", "Tim", "Will.i.am", "Leen", "Rik", "Karel", "Lisa", "Sanne", "Sven"};
        public static Color[] monkeyColors = { Color.Red, Color.Blue, Color.Yellow, Color.Orange, Color.Purple, Color.Aqua, Color.LimeGreen, Color.White, Color.DarkCyan, Color.OrangeRed };

        public static Wood wood;

        static void Main(string[] args)
        {
            // Request database password
            Boolean isConnected = false;
            while (!isConnected)
            {
                printHeader();
                Console.Write("Database password?: ");
                mysql_pass = Console.ReadLine();
                db = new DatabaseUtil(mysql_host, mysql_user, mysql_pass, mysql_data);

                int status = db.checkConnection();
                switch (status)
                {
                    case 1:
                        isConnected = true;
                        break;
                    case 1042:
                        Console.WriteLine("Unabale to create connection!");
                        Console.WriteLine(" ");
                        Console.Write("Press ENTER to continue...");
                        Console.ReadLine();
                        break;
                    case 0:
                        Console.WriteLine("Invalid password!");
                        Console.WriteLine(" ");
                        Console.Write("Press ENTER to continue...");
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }

            while(true)
            {
                printHeader();
                Console.Write("Export folder?: ");
                ExportPath = Console.ReadLine();
                if(Directory.Exists(ExportPath))
                    break;
                else
                {
                    Console.WriteLine("That folder does not exist!");
                    Console.WriteLine(" ");
                    Console.Write("Press ENTER to continue...");
                    Console.ReadLine();
                }
            }
            
            while (true) {
                // Request for user input variables
                StartupUtil.inputVariables();
            }
        }

        public static void printHeader()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Project created by Tim De Smet");
            Console.WriteLine("HoGent - Escape From The Woods");
            Console.WriteLine("--------------------------");
            Console.WriteLine(" ");
        }
    }
}
