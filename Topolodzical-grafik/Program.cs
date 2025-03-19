class Program //trošku pozdě koukám, že nějaký polotovar kód je na classroom, no co už
{
    static void Main()
    {
        string input = "d dd dha dhff dhfj dc c ca cd cc cg cb cbe cbf cee cek cf cfj cfk cfi";
        string[] words = input.Split(' ');
        Dictionary<char, HashSet<char>> graph = new(); //HashSet nesmí nidky obsahovat dva stejné prvky, tady char=ten prvek, hashset=vše co následuje po něm
        HashSet<char> seen = new(); //seen, visited, explored idk viděl jsem to pojmenované různě

        foreach (string word in words)
            foreach (char ch in word)
                seen.Add(ch);

        for (int i = 0; i < words.Length - 1; i++) //ve škole jsem nebyl, ale předpokládám, že je to na ten topologické uspořádání, takže nejprve dělám ten graf
        {
            string first = words[i], second = words[i + 1]; //definuju aktuální a následující slovo
            int minLen = Math.Min(first.Length, second.Length);

            for (int j = 0; j < minLen; j++) //postupně se zanořuji stále hlouběji do slova
            {
                if (first[j] != second[j]) //pokud jsou rozdílné tak koukám jestli už tam tohle pravidlo jednou není
                {
                    if (!graph.ContainsKey(first[j])) graph[first[j]] = new HashSet<char>();
                    graph[first[j]].Add(second[j]);
                    break;
                }
            }
        } //rozšiřující část úkolu done... a taky ta více zábavná :(

        List<char> result = TopologicalSort(graph, seen); //je to tu jen proto, že exception za boha nefungovali
        if (result == null)
        {
            Console.WriteLine("obsahuje cyklus => nejde");
        }
        else
        {
            Console.WriteLine(string.Join(" -> ", result));
        }
    }

    static List<char>? TopologicalSort(Dictionary<char, HashSet<char>> graph, HashSet<char> nodes) //děkuji https://www.youtube.com/watch?v=eL-KzMXSXXI za vysvětlení
    {
        Dictionary<char, int> inDegree = nodes.ToDictionary(n => n, n => 0);  //kolik písmen musí nutně předcházet/kolik mám hran
        foreach (var edges in graph.Values)
            foreach (var v in edges)
                inDegree[v]++;

        Queue<char> queue = new(nodes.Where(n => inDegree[n] == 0));
        List<char> order = new();

        while (queue.Count > 0)
        {
            char current = queue.Dequeue();
            order.Add(current);
            if (!graph.ContainsKey(current)) continue;

            foreach (var neighbor in graph[current]) //abychom jsme se nazacyklili
            {
                if (--inDegree[neighbor] == 0)
                    queue.Enqueue(neighbor);
            }
        }

        return order.Count == nodes.Count ? order : null;
    }
}//dopsáno 8.3. roku pána Boha 2025 v osm hodin večerních, teď si jdu dát šlofíka a pak vyrážíme na cestu Prahou, jestli se už nevrátím, můžete za to Vy