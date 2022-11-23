﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram
{
    public class CalculatorLogic
    {
        private static Calculator calculator = new();
        public static int GetMainMenuChoice()
        {
            // Display title as the C# console calculator app.
            CalculatorUI.ClearScreen();

            // Define menu choices
            string menuChoiceMessage = "Choose action from follow menu items:";
            (string, string)[] menuChoices = new[]
                {
                    ("a", "Perform math operation"),
                    ("b", "Print completed calculations"),
                    ("c", "Close application")
                };

            return UserInput.PromptChoice(menuChoiceMessage, menuChoices);
        }

        public static void PrintCalculationsSummary()
        {
            // List and format all calculations
            var tableHeaders = new string[] { "Id", "Equation", "Result" };
            var finalCalculations = new List<string>();

            var i = 1;
            foreach (var calculation in calculator.ListCompletedCalculations())
            {
                finalCalculations.Add($"{i},{calculation.Item1},{calculation.Item2}");
            }

            // Print data
            CalculatorUI.ClearScreen();
            OutputTable.PrintTable(tableHeaders, finalCalculations.ToArray());
            Console.WriteLine($"Total: {calculator.OperationsCount}");
        }

        public static void HandleMathOperation()
        {
            // Display title as the C# console calculator app.
            CalculatorUI.ClearScreen();

            // Declare variables and set to empty.
            double result = 0;

            // Ask the user to type the first and second number
            string messageFirstNumber = "Type a number, and then press Enter: ";
            string messageSecondNumber = "Type another number, and then press Enter: ";
            string errorMessageNumber = "This is not valid input. Please enter an integer value: ";

            double cleanNum1 = UserInput.PromptTypeNumber(messageFirstNumber, errorMessageNumber);
            double cleanNum2 = UserInput.PromptTypeNumber(messageSecondNumber, errorMessageNumber);

            // Ask the user to choose an operator.
            string operationChoiceMessage = "Choose an operator from the following list:";
            (string, string)[] operationChoices = new[]
                {
                    ("a", "Add"),
                    ("s", "Substract"),
                    ("m", "Multiply"),
                    ("d", "Divide")
                };
            int op = UserInput.PromptChoice(operationChoiceMessage, operationChoices);

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }
    }
}
