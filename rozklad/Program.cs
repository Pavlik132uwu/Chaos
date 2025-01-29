using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Zadejte Edvarda k rozložení:");
        int Edvard = int.Parse(Console.ReadLine());
        Console.WriteLine("Možnosti rozložení Edvarda:");
        Najitscitance(Edvard);
    }

    static void Najitscitance(int Edvard)
    {
        Stack<int> Edvardko = new Stack<int>();
        int sum = 0;

        Edvardko.Push(1);
        sum += 1;

        while (Edvardko.Count > 0)
        {
            if (sum == Edvard)
            {
                PrintStack(Edvardko); //Když máme součet, vytisknuhu ho
            }
            if (sum >= Edvard) // Pokud součet překročí Edvarda nebo jsme dosáhli výsledku, upravíme zásobník
            {
                sum -= Edvardko.Pop(); // Odeberu poslední číslo

                if (Edvardko.Count > 0)
                {
                    int top = Edvardko.Pop();
                    sum -= top;

                    if (top < Edvard)
                    {
                        Edvardko.Push(top + 1); // Zvýším předchozí číslo
                        sum += top + 1;
                    }
                }
            }
            else
            {
                int top = Edvardko.Peek(); // Přidám další číslo, které nesmí být větší než poslední, jinak bych se dostal do loopu
                Edvardko.Push(top);
                sum += top;
            }
        }
    }

    static void PrintStack(Stack<int> Edwardik)
    {
        var list = new List<int>(Edwardik);
        list.Reverse();
        Console.WriteLine(string.Join("+", list));
    }
}