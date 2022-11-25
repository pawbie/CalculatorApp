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
            string mainMenuChoice = "";
            
            while (!endApp)
            {
                // Display main menu and get user choice
                mainMenuChoice = CalculatorLogic.GetMainMenuChoice();

                switch (mainMenuChoice)
                {
                    case "mathOperation":
                        CalculatorLogic.HandleMathOperation();
                        break;
                    case "printCalculations":
                        CalculatorLogic.PrintCalculationsSummary();
                        break;
                    case "deleteCalculations":
                        CalculatorLogic.ClearCalculationsHistory();
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