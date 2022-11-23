using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram
{
    public class UserInput
    {
        
        public static double PromptTypeNumber(string message, string errorMessage)
        {
            string numInput;

            // Ask the user to type the number
            Console.Write(message);
            numInput = Console.ReadLine();

            // Loop until valid number is provided
            double cleanNum = 0;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write(errorMessage);
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        public static int PromptChoice(string message, (string, string)[] choices)
        {
            bool choiceConfirmed;
            bool choiceCorrect;
            string currentChoice = "";

            // Print initial messages
            Console.WriteLine(message);
            Console.WriteLine();
            foreach (var choice in choices)
            {
                Console.WriteLine($"{choice.Item1} - {choice.Item2}");
            }
            Console.Write($"Your option: {currentChoice}");

            // Loop until key with one of allowed letters is pressed
            choiceConfirmed = false;
            while (choiceConfirmed == false)
            {
                choiceCorrect = false;
                while (choiceCorrect == false)
                {
                    var userChoice = Console.ReadKey(true).KeyChar.ToString();
                    if (choices.Select(x => x.Item1).ToArray().Contains(userChoice))
                    {
                        currentChoice = userChoice;

                        CalculatorUI.ClearCurrentLine();
                        Console.Write($"Your option: {currentChoice}");
                        choiceCorrect = true;
                    }
                }


                choiceCorrect = false;
                while (choiceCorrect == false)
                {
                    var userConfirmationChoice = Console.ReadKey(true);
                    if (userConfirmationChoice.Key == ConsoleKey.Enter)
                    {
                        choiceCorrect = true;
                        choiceConfirmed = true;
                    }
                    else if (choices.Select(x => x.Item1).ToArray().Contains(userConfirmationChoice.KeyChar.ToString()))
                    {
                        currentChoice = userConfirmationChoice.KeyChar.ToString();

                        CalculatorUI.ClearCurrentLine();
                        Console.Write($"Your option: {currentChoice}");
                        choiceCorrect = true;

                    }
                }
            }

            Console.WriteLine();
            return choices.Select(x => x.Item1).ToList().IndexOf(currentChoice);
        }
    }
}
