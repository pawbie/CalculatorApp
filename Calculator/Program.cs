using System;
using CalculatorProgram;
using System.IO;
using System.Diagnostics;


namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare main variables
            bool endApp = false;
            int mainMenuChoice = 0;
            
            while (!endApp)
            {
                // Display main menu and get user choice
                mainMenuChoice = CalculatorLogic.GetMainMenuChoice();

                switch (mainMenuChoice)
                {
                    case 0:
                        CalculatorLogic.HandleMathOperation();
                        break;
                    case 1:
                        CalculatorLogic.PrintCalculationsSummary();
                        break;
                    default:
                        endApp = true;
                        break;
                }

                if (!endApp)
                {
                    Console.WriteLine("\nPress ANY KEY to continue...");
                    Console.ReadKey();
                }

            }
        }
    }
}