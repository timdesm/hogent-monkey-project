using System;
using System.Collections.Generic;
using System.Text;

namespace HoGent_Monkey_Project
{
    class StartupUtil
    {
        public static void inputVariables()
        {
            Boolean userCheck = false;
            while (!userCheck)
            {
                int map_x = inputInteger("Map X-coord");
                int map_y = inputInteger("Map Y-coord");
                int trees = inputInteger("Amount of trees");
                int monkeys = inputInteger("Amount of monkeys");

                Program.printHeader();
                Console.WriteLine("Input overview");
                Console.WriteLine("X: " + map_x);
                Console.WriteLine("Y: " + map_y);
                Console.WriteLine("Trees: " + trees);
                Console.WriteLine("Monkeys: " + monkeys);
                Console.WriteLine(" ");
                Console.Write("Are these values correct (Y/N)? ");
                String continueInput = Console.ReadLine();
                switch (continueInput.ToUpper())
                {
                    case "Y":
                        Program.wood = new Wood(1, map_x, map_y, trees, monkeys);
                        userCheck = true;
                        break;
                    case "N":
                        userCheck = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static int inputInteger(String type)
        {
            while(true)
            {
                Program.printHeader();
                Console.Write(type + "?: ");
                String input = Console.ReadLine();
                if(Int32.TryParse(input, out int result))
                {
                    if(result > 0)
                    {
                        return result;
                    }
                    Console.WriteLine("Number must be more than 0");
                    Console.WriteLine(" ");
                    Console.Write("Press ENTER to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Input is not a valid number");
                    Console.WriteLine(" ");
                    Console.Write("Press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}
