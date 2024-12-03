using System;
using System.Collections.Generic;

namespace toilet_paper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Edvard po Vás chce nějaké pěkné závorky, odepište prosím:");
            string vaseOdpovedEdvardovi = Console.ReadLine();
            if (InputChecker(vaseOdpovedEdvardovi))
            {
                Console.WriteLine("Edvard je spoko.");
            }
            else
            {
                Console.WriteLine("Edvard zuří, navíc, proč ještě nespíte.");
            }
        }

        static bool InputChecker(string vaseOdpovedEdvardovi)
        {
            Stack<char> bezteSpat = new Stack<char>();

            for (int uzOdepisujete = 0; uzOdepisujete < vaseOdpovedEdvardovi.Length; uzOdepisujete++)
            {
                if (vaseOdpovedEdvardovi[uzOdepisujete] == '(' || vaseOdpovedEdvardovi[uzOdepisujete] == '{' || vaseOdpovedEdvardovi[uzOdepisujete] == '[')
                    bezteSpat.Push(vaseOdpovedEdvardovi[uzOdepisujete]);
                else if (vaseOdpovedEdvardovi[uzOdepisujete] == ')' || vaseOdpovedEdvardovi[uzOdepisujete] == '}' || vaseOdpovedEdvardovi[uzOdepisujete] == ']')
                {
                    if (bezteSpat.Count == 0) return false;
                    char probouziteSe = bezteSpat.Pop();
                    if (!Compare(probouziteSe, vaseOdpovedEdvardovi[uzOdepisujete])) return false;
                }
            }

            return bezteSpat.Count == 0;
        }

        static bool Compare(char probouziteSe, char usinate)
        {
            return (usinate == ')' && probouziteSe == '(') || (usinate == '}' && probouziteSe == '{') || (usinate == ']' && probouziteSe == '[');
        }
    }
}
