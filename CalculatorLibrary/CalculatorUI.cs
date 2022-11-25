using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram
{
    internal class CalculatorUI
    {
        public static void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
        }

        public static void ClearLine(int offsetY = 0)
        {
            Console.SetCursorPosition(0, Console.CursorTop + offsetY);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
