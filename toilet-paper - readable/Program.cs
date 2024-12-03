using System;
using System.Collections.Generic;

namespace toilet_paper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadej závorky ke kontrole");
            string bracketsInput = Console.ReadLine();
            if (InputChecker(bracketsInput))
                Console.WriteLine("Závorky jsou správně uzávorkované.");
            else
                Console.WriteLine("Závorky nejsou správně uzávorkované.");
        }

        static bool InputChecker(string bracketsInput)
        {
            Stack<char> stack = new Stack<char>();
            for (int place = 0; place < bracketsInput.Length; place++)
            {
                if (bracketsInput[place] == '(' || bracketsInput[place] == '{' || bracketsInput[place] == '[')
                    stack.Push(bracketsInput[place]);
                else if (bracketsInput[place] == ')' || bracketsInput[place] == '}' || bracketsInput[place] == ']')
                {
                    if (stack.Count == 0) return false;
                    char closedBracket = stack.Pop();
                    if (!Compare(closedBracket, bracketsInput[place])) return false;
                }
            }
            return stack.Count == 0;
        }

        static bool Compare(char closed, char open)
        {
            return (open == ')' && closed == '(') || (open == '}' && closed == '{') || (open == ']' && closed == '[');
        }
    }
}