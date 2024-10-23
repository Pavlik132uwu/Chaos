namespace Linked_lists //V tomto projektu v rámci vytvořené třídy pro jednosměrný spojový seznam dále implementujte funkci na: Nalezení minima ve spojovém seznamu(50b) - Upozornění: V jednom seznamu se hodnoty mohou opakovat.Seznamy mohou být i prázdné.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList linkedList = new LinkedList();
            int? min = linkedList.FindMin(); //pokud to chápu správně, funkce měla minimum pouze nacházet ne i vypisovat, pokud to tedy chceme udělat, je to nutné provést mimo funkci
            if (min == null)
                Console.WriteLine("List je prázdný");
            else
                Console.WriteLine($"Minimum v seznamu: {min}");
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
                Node current = Head.Next;

                while (current != null)
                {
                    if (current.Value < min)
                    {
                        min = current.Value;
                    }
                    current = current.Next;
                }

                return min;
            }
        }
    }
}
