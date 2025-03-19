using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BInarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            Console.WriteLine(tree.Find(20).Value);
            Console.WriteLine(tree.Min().Value);
            Console.WriteLine(tree.Max().Value);

            // Create a new student
            Student newStudent = new Student(101, "Vojta", "Vomáčka", 99, "5.C");
            // Add the student to the binary search tree
            tree.Insert(newStudent.Id, newStudent);

            tree.Delete(20);

            Console.WriteLine(tree.Show());
            Console.WriteLine(tree.Find(101).Value);

            tree.DeleteEven();

            Console.WriteLine(tree.Show());
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


        }
    }

    // Na uzel si vytvoříme třídu, protože u něj chceme sledovat vícero vlastností: klíč, uloženou hodnotu, levý a pravý syn v budoucím stromě
    // uzel sám o sobě však nic nedělá => nemá žádné metody
    class Node<T> // Použijeme generický typ T, aby hodnota v uzlu mohla být cokoli (číslo, text, vlastní datový typ...) si chceme organizovaně ukládat
    {
        public int Key { get; set; }
        // aby se jednalo o vlastnost, použijeme tzv. getter a setter
        // oba jsou public, jelikož klíč uzlu budeme potřebovat měnit z metod stromu
        // veřejná vlastnost vždy začíná velkým písmenem
        public T Value { get; set; }
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            LeftSon = null; RightSon = null;
        }

        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }
    }

    class BinarySearchTree<T> // zde potřebujeme opět uvést generický typ T a podle toho vytářet uzly stromu daného typu
    {
        public Node<T> Root { get; private set; } // kořen stromu nastavujeme interně, ne z žádné jiné třídy => private set

        /// <summary>
        /// Inserts new node into Binary Search Tree with specified key and value. 
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="newValue"></param>
        /// <returns>Returns True if node was successfully inserted or False if inserted key was already present.</returns>
        
        /// nejsem si jistý jestli jde reutnovat True/False protože už Vámi v zadání je Insert jako void
        public void Insert(int newKey, T newValue)
        {
            Node<T> _insert(Node<T> node, int key, T value)
            {
                if (node == null)
                    return new Node<T>(key, value);

                if (key < node.Key)
                    node.LeftSon = _insert(node.LeftSon, key, value);
                else if (key > node.Key)
                    node.RightSon = _insert(node.RightSon, key, value);
                else
                    return node; // Handle duplicate keys by returning the existing node

                return node;
            }

            if (Root == null)
            {
                Root = new Node<T>(newKey, newValue);
            }
            else
                _insert(Root, newKey, newValue);
        }

        public Node<T> Find(int key)
        {
            Node<T> _find(Node<T> node, int key2) // privátní funkci mohu založit i uvnitř jiné funkce. Je pak viditelná, jen z té vnější funkce
            {
                if (node == null)
                    return null;
                if (key == node.Key)
                    return node;
                else if (key > node.Key)
                    return _find(node.RightSon, key);
                else
                    return _find(node.LeftSon, key);
            }
            return _find(Root, key);
        }


        public string Show()
        {
            void _show(Node<T> node, StringBuilder nodes)
            {
                if (node != null)
                {
                    _show(node.LeftSon, nodes);
                    nodes.Append(node.Key); //využijeme StringBuilder, abychom nevytvářely s každým dalším klíčem nový string => časově i paměťově daleko méně náročné než += u stringu
                    nodes.Append(" ");
                    _show(node.RightSon, nodes);
                }
            }
            StringBuilder sb = new StringBuilder();
            _show(Root, sb);
            return sb.ToString(); // výpis ponecháme jednou naráz v Mainu, WriteLine do konzole je časově drahá operace
        }

        public Node<T> Max()
        {
            return _max(Root);
        }
        private Node<T> _max(Node<T> node)
        {
            if (node.RightSon == null)
                return node;
            return _max(node.RightSon);
        }

        public Node<T> Min()
        {
            return _min(Root);
        }
        private Node<T> _min(Node<T> node)
        {
            if (node.LeftSon == null)
                return node;
            return _min(node.LeftSon);
        }
        private Node<T> FindMin(Node<T> node)
        {
            while (node.LeftSon != null)
                node = node.LeftSon;
            return node;
        }

        public void Delete(int key)
        {
            Node<T> _delete(Node<T> node, int targetKey)
            {
                if (node == null)
                    return node;

                if (targetKey < node.Key)
                    node.LeftSon = _delete(node.LeftSon, targetKey); // Search in left subtree
                else if (targetKey > node.Key)
                    node.RightSon = _delete(node.RightSon, targetKey); // Search in right subtree
                else
                {
                    // Node to be deleted is found

                    // Case 1: Node has no children (leaf node)
                    if (node.LeftSon == null && node.RightSon == null)
                        return null; // Remove the node by returning null

                    // Case 2: Node has one child
                    if (node.LeftSon == null)
                        return node.RightSon; // Replace node with its right child
                    else if (node.RightSon == null)
                        return node.LeftSon; // Replace node with its left child

                    // Case 3: Node has two children
                    // Find the in-order successor (the smallest node in the right subtree)
                    Node<T> successor = FindMin(node.RightSon);  // Renamed _min to FindMin
                    node.Key = successor.Key; // Replace node's key with the successor's key
                    node.Value = successor.Value; // Replace node's value with the successor's value
                    node.RightSon = _delete(node.RightSon, successor.Key); // Delete the successor
                }
                return node; // Return the (possibly updated) node
            }

            // Start deletion from the root
            Root = _delete(Root, key);
        }
        public void DeleteEven()
        {
            void _deleteEven(Node<T> node)
            {
                if (node == null)
                    return;

                // Recursively check the left subtree
                _deleteEven(node.LeftSon);

                if (node.Key % 2 == 0)
                {
                    Delete(node.Key);
                }

                _deleteEven(node.RightSon);
            }

            // Start traversal from the root
            _deleteEven(Root);
        }
    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}
