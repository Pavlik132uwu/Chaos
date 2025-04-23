using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cislicka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Pokud chcete vyhodnotit zápis v prefixu napište pre, pokud v postfixu napište post");
                string fix = Console.ReadLine();
                if (fix == "pre")
                {
                    Console.WriteLine("Zadejte výraz v prefixu:");
                    string expression= Console.ReadLine();
                    //funkce pro prefix
                    break;
                }
                if (fix == "post")
                {
                    Console.WriteLine("Zadejte výraz v postfixu:");
                    string expression = Console.ReadLine();
                    float result = Expression.Postfix(expression);
                    break;
                }
                else
                {
                    Console.WriteLine("Zadejte platný vstup");
                }
            }
        }
    }
    class Expression
    {
        public static float Postfix(string expression)
        {
            Stack<float> stack = new Stack<float>();
            string[] strToStack = expression.Split(' ');
            foreach (string str in strToStack) {
            {
                    //přidává věci do stacku a vyhodnocuje věcičky...
            }
        }
    }
}
