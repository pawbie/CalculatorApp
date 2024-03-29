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
        public static string GetMainMenuChoice()
        {
            // Display title as the C# console calculator app.
            CalculatorUI.ClearScreen();

            // Define menu choices
            (string, string, string)[] menuChoices;
            string menuChoiceMessage = "Choose action from follow menu items:";
            if (calculator.OperationsCount > 0)
            {
                menuChoices = new[]
                {
                    ("a", "Perform math operation", "mathOperation"),
                    ("b", "Print completed calculations", "printCalculations"),
                    ("c", "Delete completed calculations", "deleteCalculations"),
                    ("d", "Close application", "exitApp")
                };
            }
            else
            {
                menuChoices = new[]
                {
                    ("a", "Perform math operation", "mathOperation"),
                    ("b", "Close application", "exitApp")
                };
            }
            
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

        public static void ClearCalculationsHistory()
        {
            CalculatorUI.ClearScreen();

            var clearHistory = UserInput.PromptContinue("Are you sure you want to delete completed calculations?");
            if (clearHistory == true)
            {
                calculator.ClearCompletedCalculations();
            }
        }

        public static double GetMathOperand(int operandOrder)
        {
            string numberSource = "";
            double[] pastResults;
            double definedNumber;

            var header = operandOrder == 1 ? "FIRST NUMBER" : "SECOND NUMBER";
            var choicesMessage = "Please choose preferred number source:";
            var typeNumerMessage = "Type a number, and then press Enter: ";
            var typeNumberErrorMessage = "This is not valid input. Please enter an integer value: ";
            var selectNumberMessage = "Select result from previous calculations. Use UP/DOWN arrows to browse: ";

            CalculatorUI.ClearScreen();
            Console.WriteLine(header);
            if (calculator.OperationsCount > 0)
            {
                var menuChoices = new[]
                {
                    ("a", "Type number manually", "typeNumber"),
                    ("b", "Select result from previous operation", "selectResultNumber"),
                };
                numberSource = UserInput.PromptChoice(choicesMessage, menuChoices);
            }

            CalculatorUI.ClearScreen();
            Console.WriteLine(header);
            switch (numberSource)
            {
                case "typeNumber":
                    definedNumber = UserInput.PromptTypeNumber(typeNumerMessage, typeNumberErrorMessage);
                    break;
                case "selectResultNumber":
                    pastResults = calculator.ListCompletedCalculations().Select(x => x.Item2).ToArray();
                    definedNumber = UserInput.PromptSelectListItem(selectNumberMessage, pastResults);
                    break;
                default:
                    definedNumber = UserInput.PromptTypeNumber(typeNumerMessage, typeNumberErrorMessage);
                    break;
            }

            return definedNumber;
        }

        public static void HandleMathOperation()
        {
            // Display title as the C# console calculator app.
            CalculatorUI.ClearScreen();

            // Declare variables and set to empty.
            double result = 0;

            // Ask the user to type the first and second number
            double cleanNum1 = GetMathOperand(1);
            double cleanNum2 = GetMathOperand(2);

            // Ask the user to choose an operator.
            CalculatorUI.ClearScreen();
            string operationChoiceMessage = "Choose an operator from the following list:";
            (string, string, string)[] operationChoices = new[]
                {
                    ("a", "Add", "add"),
                    ("s", "Substract", "substract"),
                    ("m", "Multiply", "multiply"),
                    ("d", "Divide", "divide")
                };
            string op = UserInput.PromptChoice(operationChoiceMessage, operationChoices);

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
