using System;
using System.Collections.Generic;

namespace TicketShop
{
    public static class MyExtensions
    {
        public static void ToConsole<T>(this IEnumerable<T> input, string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\tBEGIN: " + str);
            Console.ResetColor();

            foreach (T item in input) Console.WriteLine(item.ToString());

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine('\t' + str + " END.\t(Press a key)");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}