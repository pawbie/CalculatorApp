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

        public static bool PromptContinue(string message)
        {
            var userInput = "";

            while (userInput.ToLower() != "y" && userInput.ToLower() != "n")
            {
                if (string.IsNullOrEmpty(userInput) == false)
                {
                    CalculatorUI.ClearLine(-1);
                }

                Console.Write($"{message} [y/n]: ");
                userInput = Console.ReadLine();
            }

            return userInput.ToLower() == "y";
        }
        public static T PromptSelectListItem<T>(string message, T[] choiceList)
        {
            int selectionIndex = -1;
            bool selectionConfirmed = false;

            Console.Write(message);
            while (selectionConfirmed == false)
            {
                var pressedKey = Console.ReadKey().Key;
                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        if (selectionIndex <= 0)
                        {
                            selectionIndex = choiceList.Length - 1;
                        }
                        else
                        {
                            selectionIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectionIndex == choiceList.Length - 1)
                        {
                            selectionIndex = 0;
                        }
                        else
                        {
                            selectionIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (selectionIndex >= 0) selectionConfirmed = true;
                        break;
                }

                if (selectionIndex >= 0 && selectionConfirmed == false)
                {
                    CalculatorUI.ClearLine();
                    Console.Write($"{message}{choiceList[selectionIndex]}");
                }

            }

            return choiceList[selectionIndex];
        }

        public static string PromptChoice(string message, (string choiceLetter, string choiceDescription, string choiceAction)[] choices)
        {
            // Get array of choiceLetters only
            string[] choiceLetters = choices.Select(x => x.choiceLetter).ToArray();

            // Setup initial valus for validation variables
            var choiceConfirmed = false;
            var choiceCorrect = false;
            var currentChoice = "";

            // Print initial messages
            Console.WriteLine(message);
            Console.WriteLine();
            foreach (var choice in choices)
            {
                Console.WriteLine($"{choice.choiceLetter} - {choice.choiceDescription}");
            }
            Console.Write($"Your option: {currentChoice}");

            // Loop until key with one of allowed letters is pressed
            while (choiceConfirmed == false)
            {
                var userChoice = Console.ReadKey(true);
                if (userChoice.Key == ConsoleKey.Enter)
                {
                    if (choiceCorrect) choiceConfirmed = true;
                }
                else if (choiceLetters.Contains(userChoice.KeyChar.ToString()))
                {
                    currentChoice = userChoice.KeyChar.ToString();

                    CalculatorUI.ClearLine();
                    Console.Write($"Your option: {currentChoice}");
                    choiceCorrect = true;
                }

            }

            Console.WriteLine();
            return choices.First(x => x.choiceLetter == currentChoice).choiceAction;
        }
    }
}
