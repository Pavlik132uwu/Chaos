# List < T >
 > vysvětlím co jednotlivé vestavěné funcke dělají a jakou mají časovou složitost
## Add
 **funkce:** na konec listu přidá nový prvek
 **časová složitost:** většinou se jedná o O(1), ale pokud přesouváme list do většího uložiště/pole tak se jedná o O(n)
## Contains
 **funkce:** vyhodnotí (a vrátí bool) podle toho zda list obsahuje prvek
 **časová složitost:** O(n) - prohledávání od začátku
## Insert
 **funkce:**  O(n) - samotné vložení trvá vždy stejně O(1), ale tím, že je nutné posunou všechny následující je to O(n)
 **časová složitost:** přidá prvek na určité místo (index), prvky po něm posune o jeden index nahoru
## Remove
 **funkce:** odstraní prvek ze seznamu a násedné prvky o posune jeden index dolů
 **časová složitost:** O(n), stejně jako u insert manipuluje se všemi prvky po zadaném prvku, záleží tedy na jejich počtu
# Linked List
 **Popis:** skládá se z uzlů, které mimo data obsahují i odkazy (nebo ukazatele) na další uzly, to umožňuje efektivnější manipulaci
 **Porovnání s LinkedList< T >:** linked  list je obecný název pro spojový seznam kdežto LinkedList< T > je jeho využití v C#, to je součástí standartních vestavěných funkcí a zároveň nabízí různé metody pro jeho manipulaci. Musíme předem definovat datové typy které bude obsahovat.
 **Nabízené funkce:** Pro spojový seznam můžeme použít obecně funkce podobné pro klasické seznam, tedy přidání, odebrání, vyhledávání atd.
 **Časová složitost:** Na některý funkcích LinkedList< T > si ukážeme časovou složitost:
 1. **AddFirst/*Last*(Prvek)- O(1):** přidá prvek na začátek/konec seznamu, ten se ale nemusí nijak posouvat, proto O(1) 
 2. **AddBefore/*After*(index, Prvek):** přidá prvek na určité místo (před/po indexu)
 3. **Find (Prvek)** - i u spojového seznamu se jedná o časovou náročnost O(n)
 4. **Count** - O(1) - tento list udržuje počet prvků a tedy nezáležá na jejich počtu
 5. **...:** existují další funkce ale nechtěl jsem je všechny vypisovat
# Funkce na nalezení prvku
## Implementace funkce
> používám array, protože to více odpovídá zadání které říká "bude v zadaném poli integerů" ale mohl bych klidně použít i listy když tu o nich mluvím
```
public static int Hledej(int[] array, int target)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == target)
        {
            return i;
        }
    }
    return -1;
}
```
## Zavolání funkce
```
public static void Main()
{
    int[] numbers = {1, 2, 5, 7, 12, 0};
    int target = 12;
    int result = Hledej(numbers, target);
    Console.WriteLine(result);
}
```
Zdroje [^1][^2][^3][^4]
[^1]: https://www.freecodecamp.org/news/big-o-cheat-sheet-time-complexity-chart/#:~:text=Big%20O%20Notation%20is%20a%20metric%20for%20determining,scales%20as%20the%20size%20of%20your%20input%20increases.
[^2]: https://www.geeksforgeeks.org/c-sharp-list-class/
[^3]: https://www.geeksforgeeks.org/linked-list-data-structure/
[^4]: https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/arrays-and-collections