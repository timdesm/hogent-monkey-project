using System;

namespace HoGent_Monkey_Project
{
    class Program
    {
        public static DatabaseUtil db;
        public static String mysql_host = "timdesmet.be";
        public static String mysql_user = "u32002p26917_hogent";
        public static String mysql_pass;
        public static String mysql_data = "u32002p26917_hogent";

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

            // Request for user input variables
            StartupUtil.inputVariables();

            printHeader();
            while (true) { }
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
