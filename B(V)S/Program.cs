using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace B_V_S
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node<string> node1 = new Node<string>(1, "Čau");
            Node<string> node2 = new Node<string>(2, "Zdravim");
            Node<string> node3 = new Node<string>(3, "Ahoj");
            Node<string> node4 = new Node<string>(4, "Ahojda");
            Node<string> node5 = new Node<string>(5, "Ahojdadad");

            node1.LSon = node2;
            node1.RSon = node3;

            node3.LSon = node4;
            node3.RSon = node5;

            BST<string> tree = new BST<string>();
            tree.Root = node1;

            Console.WriteLine(tree.Show());
            Console.WriteLine(tree.Find(57));
        }
    }
    class Node<T>
    {
        public T Value { get; set; }
        public int Key { get; set; }
        public Node<T> LSon { get; set; } = null;
        public Node<T> RSon { get; set; } = null;
        
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
    }
    class BST<T>
    {
        public Node<T> Root { get; set; }
        public string Show()
        {
            string output = "";
            void _show(Node<T> node)
            {
                if (node == null) return;
                _show(node.LSon);
                output += node.Key.ToString() + " ";
                _show(node.RSon);
            }
            if (Root == null)
                return "Nic tu není";
            _show(Root);
            return output;
        }
        public T Find(int key)
        {
            Node<T> _find(Node<T> node, int key2)
            {
                if (node == null) return null;

                if (node.Key == key2) return node;

                if (key2 < node.Key) return _find(node.LSon, key2);
                else
                    return _find(node.RSon, key2);
            }

            Node<T> output = _find(Root, key);
            if (output == null)
                return default(T);
            return _find(Root, key).Value;
        }
    }
}
