using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Načtení matice vzdáleností
        Console.WriteLine("Zadejte matici vzdáleností (řádky oddělené enterem, hodnoty mezerou, -1 pro žádnou hranu):");
        List<int[]> matrixInput = new List<int[]>();
        string line;

        // Read matrix rows until an empty line is entered
        while (true)
        {
            line = Console.ReadLine().Trim();  // Remove leading/trailing whitespace
            if (string.IsNullOrEmpty(line)) break;  // Stop when an empty line is entered
            try
            {
                // Add row to matrix if it's valid
                matrixInput.Add(line.Split(' ').Select(int.Parse).ToArray());
            }
            catch (FormatException)
            {
                Console.WriteLine("Neplatný vstup, zadejte čísla oddělená mezerou.");
                return;
            }
        }

        // Check if the matrix is empty or rows are inconsistent
        if (matrixInput.Count == 0)
        {
            Console.WriteLine("Nebyla zadána žádná matice.");
            return;
        }

        // Ensure all rows have the same length
        int rowLength = matrixInput[0].Length;
        if (matrixInput.Any(row => row.Length != rowLength))
        {
            Console.WriteLine("Všechny řádky matice musí mít stejný počet sloupců.");
            return;
        }

        // Create the matrix based on the input
        int[,] distanceMatrix = new int[matrixInput.Count, matrixInput.Count];
        for (int i = 0; i < matrixInput.Count; i++)
        {
            for (int j = 0; j < matrixInput[i].Length; j++)
            {
                distanceMatrix[i, j] = matrixInput[i][j];
            }
        }

        // Načtení jmen
        Console.WriteLine("Zadejte jména studentů oddělená středníkem:");
        string[] names = Console.ReadLine().Split(';').Select(name => name.Trim()).ToArray();

        // Načtení počátečního studenta
        Console.WriteLine("Zadejte jméno počátečního studenta:");
        string startName = Console.ReadLine().Trim();

        int n = distanceMatrix.GetLength(0);
        int startIndex = Array.IndexOf(names, startName);

        if (startIndex == -1)
        {
            Console.WriteLine("Zadané jméno není v seznamu studentů.");
            return;
        }

        // Výstupní matice
        int[,] resultMatrix = new int[n, n];

        // Dijkstra
        int[] distances = Enumerable.Repeat(int.MaxValue, n).ToArray();
        bool[] visited = new bool[n];
        int[] previous = new int[n];
        Array.Fill(previous, -1);

        distances[startIndex] = 0;

        for (int i = 0; i < n; i++)
        {
            int current = -1;
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && (current == -1 || distances[j] < distances[current]))
                {
                    current = j;
                }
            }

            if (current == -1 || distances[current] == int.MaxValue)
                break;

            visited[current] = true;

            for (int neighbor = 0; neighbor < n; neighbor++)
            {
                if (distanceMatrix[current, neighbor] != -1 && !visited[neighbor])
                {
                    int newDist = distances[current] + distanceMatrix[current, neighbor];
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previous[neighbor] = current;
                    }
                }
            }
        }

        // Naplnění výstupní matice
        for (int i = 0; i < n; i++)
        {
            if (previous[i] != -1)
            {
                resultMatrix[previous[i], i] = 1;
            }
        }

        // Výpis výsledné matice
        Console.WriteLine("Výsledná matice:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(resultMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}