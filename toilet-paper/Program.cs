using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toilet_paper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadej závorky ke kontrole");
            string bracketsInput = Console.ReadLine();
            Stack<char> bracketsSplit = new Stack<char>();
            for (int i = 0; i < bracketsInput.Length; i++)
            {
                bracketsSplit.Push(bracketsInput[i]);
            }
            while (true) 
            {
                char current = bracketsSplit.Pop();
                if (current == ")");
                
            //char[] bracketsSplit = bracketsInput.ToCharArray();
            // bracketsInput[0]
            //Console.WriteLine(bracketsSplit[0]);
        }
    }
}
