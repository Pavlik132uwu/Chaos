using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Linked_lists //V tomto projektu v rámci vytvořené třídy pro jednosměrný spojový seznam dále implementujte funkci na: Nalezení minima ve spojovém seznamu(50b) - Upozornění: V jednom seznamu se hodnoty mohou opakovat.Seznamy mohou být i prázdné.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            linkedList.Add(4);
            linkedList.Add(1);
            linkedList.Add(0);
            linkedList.Add(2);
            linkedList.Add(3);
            linkedList.Add(2);

            LinkedList secondLinkedList = new LinkedList();
            secondLinkedList.Add(0);
            secondLinkedList.Add(2);
            secondLinkedList.Add(1);
            secondLinkedList.Add(8);

            int? min = linkedList.FindMin(); //pokud to chápu správně, funkce měla minimum pouze nacházet ne i vypisovat, pokud to tedy chceme udělat, je to nutné provést mimo funkci
            if (min == null)
                Console.WriteLine("List je prázdný");
            else
                Console.WriteLine($"Minimum v seznamu: {min}");

            string allValues = linkedList.PrintLinkedList();
            if (allValues == null)
                Console.WriteLine("List je prázdný");
            else
                Console.WriteLine(allValues);

            linkedList.SortLinkedList();
            allValues = linkedList.PrintLinkedList();
            if (allValues == null)
                Console.WriteLine("List je prázdný");
            else
                Console.WriteLine(allValues);

            string prunik = linkedList.DestructivlyIntersectLinkedLists(secondLinkedList);
            Console.WriteLine("Destruktivní průnik spojovych seznamů: " + prunik);

            string sjednocení = linkedList.DestructivlyUnifyLinkedLists(secondLinkedList);
            Console.WriteLine("Destruktivní sjednocení spojovych seznamů: " + sjednocení);
        }

        class Node // Node je náš název pro třídu reprezentující jeden prvek spojového seznamu
                   // tvoříme tak vlastní datový typ
        {
            public Node(int value) // konstruktor třídy Node - volá se při vytváření nové instance
            {
                Value = value;
            }
            public int Value { get; }
            // public - tato vlastnost je vidět i z jiné třídy ->  díky tomu ji můžeme používat ve třídě LinkedList
            // int - je celočíselného typu
            // Value - toto je název vlastnosti reprezentující Hodnotu toho prvku seznamu
            // { get; } - tím říkáme, že hodnotu lze dále v kódu jen přečíst/získat (read-only), ale nelze ji už přenastavit
            // poslední místo, kde ji můžeme nastavit je v konstruktoru, což jsme také udělali
            public Node Next { get; set; }
            // Node - vlastnost Next je typu Node - je to taková rekurzivní definice :)
            // Next - název vlastnosti označující ukazatel na další prvek seznamu
            // { get; set; } - tato vlastnost lze číst i měnit kdekoli v kódu
            // výchozí hodnota je null (což platí pro každý vlastní datový typ)
        }
        class LinkedList // LinkedList je náš název pro třídu reprezentující samotný spojový seznam
        {
            public Node Head { get; set; }
            // pro spojový seznam si stačí pamatovat odkaz na první prvek -> hlavu Head
            // { get; set; } - tato vlastnost lze číst i měnit kdekoli v kódu
            // výchozí hodnota je null

            public void Add(int value) // metoda pro přidání prvku na začátek seznamu
            {
                if (Head == null) // když seznam je zatím prázdný
                    Head = new Node(value); // vložíme do ukazatele na první prvek (Head) nový prvek typu Node
                                            // všimněte si, že to je to místo, kde voláme konstruktor třídy Node s parametrem value - hodnotou, kterou má mít nový prvek
                else // v seznamu už něco je
                {
                    Node newNode = new Node(value); // vytvoříme nový prvek typu Node
                    newNode.Next = Head; // jeho ukazatel na další prvek (Next) nastavíme na prvek, kam ukazovala hlava seznamu -> přidáváme před původní první prvek
                    Head = newNode; // přehodíme hlavu, aby ukazovala na nový první prvek
                }
            }

            public bool Find(int value) // funkce pro hledání prvku s hodnotou v parametru
                                        // vrací bool -> true/false, podle toho, jestli bylo hledání úspěšné
            {
                Node node = Head; // vytvoříme si pomocnou proměnnou, ve které bude aktuální prohlížený prvek. Na začátku je jím hlava, tedy prvek první.
                while (node != null) // dokud nedojedeme na konec seznamu
                {
                    if (node.Value == value) // pokud jsme našli prvek se stejnou hodnotou, jako hledáme
                        return true; // vrátíme true
                    node = node.Next; // jinak se posouváme v seznamu dál na následující prvek
                }
                return false;
            }
            public int? FindMin() //funguje téměř 1:1 s Find protože prostě projíždímě prvky (u find dokud není splněna podmínka u findMin je nutjý celý)
            {
                if (Head == null)
                    return null;

                int min = Head.Value;
                Node node = Head.Next;

                while (node != null)
                {
                    if (node.Value < min)
                    {
                        min = node.Value;
                    }
                    node = node.Next;
                }

                return min;
            }
            public string PrintLinkedList() //čas: O(n)
            {
                Node node = Head;
                string allValues = "";
                if (node == null)
                    allValues = null;
                while (node != null)
                {
                    allValues += node.Value;
                    allValues += " ";
                    node = node.Next;
                }
                return allValues;
            }
            //tady začínají funkce pro sortění
            public void SortLinkedList() //čas: O(n log(n) - že já to dělal tímhle mohl jsem mít krásný bubble sort a tolik se s tím neštvat (můžu nějaký malý bonus za ty útrapy, prosím pěkně)
            {
                if (Head == null || Head.Next == null)
                    return;

                Head = MergeSort(Head);
            }

            private Node MergeSort(Node head)
            {
                if (head == null || head.Next == null)
                    return head;

                Node middle = GetMiddle(head);
                Node nextOfMiddle = middle.Next;
                middle.Next = null; // tady vlastně přetrhnu to napojení na další prvek a vytvořím dva podlisty

                Node firstHalve = MergeSort(head); //pro každou stranu opakuju
                Node secondHalve = MergeSort(nextOfMiddle);

                return SortedMerge(firstHalve, secondHalve);
            }

            private Node GetMiddle(Node head)
            {
                if (head == null) //k tomuto se chci dostat, tedy kdy už listy 
                    return head;

                Node turtle = head, Achilles = head.Next;
                while (Achilles != null && Achilles.Next != null)
                {
                    turtle = turtle.Next;
                    Achilles = Achilles.Next.Next;
                }
                return turtle;
            }

            private Node SortedMerge(Node firstHalve, Node secondHalve)
            {
                if (firstHalve == null)
                    return secondHalve;
                if (secondHalve == null)
                    return firstHalve;

                Node result;
                if (firstHalve.Value <= secondHalve.Value)
                {
                    result = firstHalve;
                    result.Next = SortedMerge(firstHalve.Next, secondHalve);
                }
                else
                {
                    result = secondHalve;
                    result.Next = SortedMerge(firstHalve, secondHalve.Next);
                }

                return result;
            }
            //konec funkcí pro sortění
            public string DestructivlyIntersectLinkedLists(LinkedList otherList) // čas O(n) jakoby n+m protože druhý list ale chápeme
            {
                if (Head == null || otherList.Head == null)
                    return "bez průniku";

                Dictionary<int, int> countInFirstList = new Dictionary<int, int>(); //abych se ujistil správný průnik, s funkcí find se dělo to, že jsem nemohl správně odebírat prvky a pak došlo např. k průniku dvou stejných hodnot i když byly dvakrát jen v prvním listu a v druhém jen jednou
                Node current = Head;

                while (current != null)
                {
                    if (countInFirstList.ContainsKey(current.Value))
                        countInFirstList[current.Value]++;
                    else
                        countInFirstList[current.Value] = 1;

                    current = current.Next;
                }

                Dictionary<int, int> countInSecondList = new Dictionary<int, int>(); //stejné jak pro list n ale akorát s values pro m
                current = otherList.Head;

                while (current != null)
                {
                    if (countInSecondList.ContainsKey(current.Value))
                        countInSecondList[current.Value]++;
                    else
                        countInSecondList[current.Value] = 1;

                    current = current.Next;
                }

                StringBuilder resultBuilder = new StringBuilder(); //prý je lepší to dávat do nějaké téhle lepšověci ne jen do stringu, ale bez mučení se přiznám, že můj výmysl to nebyl

                foreach (var pair in countInFirstList) //pár hodnoty a klíče v Dictionary
                {
                    int value = pair.Key;
                    if (countInSecondList.ContainsKey(value))
                    {
                        int countInFirst = pair.Value;
                        int countInSecond = countInSecondList[value];
                        int minCount = Math.Min(countInFirst, countInSecond);
                        for (int i = 0; i < minCount; i++) //přidá klíč tolikrát kolikrát je min hodnot z prvního a druhého listu
                            resultBuilder.Append(value).Append(",");
                    }
                }
                return resultBuilder.Length == 0 ? "bez průniku" : resultBuilder.ToString().TrimEnd(','); //fancy funkce co se postará o to, že pokud není průnik tak se to napíše jinak se vrátí klasicky hodnoty
            }
            public string DestructivlyUnifyLinkedLists(LinkedList otherList) // čas O(n) zase to n+m
            {
                if (Head == null || otherList.Head == null)
                    throw new InvalidOperationException("Jeden ze seznamů je prázdný.");

                HashSet<int> uniqueValues = new HashSet<int>(); //abych se ujistil, že je to tam poprvé
                StringBuilder resultBuilder = new StringBuilder();

                Node current = Head;
                while (current != null)
                {
                    if (uniqueValues.Add(current.Value))
                    {
                        resultBuilder.Append(current.Value).Append(",");
                    }
                    current = current.Next;
                }

                current = otherList.Head; //takhle můžu pokračovat do nekonečna ve sjednocování listů
                while (current != null)
                {
                    if (uniqueValues.Add(current.Value))
                    {
                        resultBuilder.Append(current.Value).Append(",");
                    }
                    current = current.Next;
                }
                return resultBuilder.ToString().TrimEnd(',');
            }
        }
    }
}
