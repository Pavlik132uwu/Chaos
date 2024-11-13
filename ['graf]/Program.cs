using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace __graf_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt16(Console.ReadLine());
            string friends = Console.ReadLine();
            string chainBetween = Console.ReadLine();

            bool[,] friendTable = new bool[count, count];

            // Populate the table
            foreach (string pair in friends.Split(' '))
            {
                string[] splitPair = pair.Split('-');
                int typek1 = Convert.ToInt16(splitPair[0]);
                int typecek2 = Convert.ToInt16(splitPair[1]);

                friendTable[typek1, typecek2] = true;
                friendTable[typecek2, typek1] = true;
            }

            // Processing wanted friends
            string[] wantedPair = chainBetween.Split(' ');
            int start = Convert.ToInt16(wantedPair[0]);
            int end = Convert.ToInt16(wantedPair[1]);

            Dictionary<int, bool> currentCycle = new Dictionary<int, bool>();
            Dictionary<int, bool> nextCycle = new Dictionary<int, bool>();

            currentCycle[start] = true;

            foreach (int door in currentCycle.Keys)
            {
                if (friendTable[door, end] == true)
                    return true;

                for (int i=0; i<count; i++)
                {
                    if (friendTable[door, i] == true)
                    {
                        nextCycle[i] = true;
                    }
                }
            }

            currentCycle = nextCycle;
            nextCycle = new Dictionary<int, bool>();
        }
    }

    class FriendTableFind
    {
        public bool[,] friendTable { get; }
        public int start { get; set; }
        public int end { get; set; }


    }

    public static bool HaveConnectionOneCycle( int end, Dictionary<int, bool> currentCycle)
    {
        Dictionary<int, bool> nextCycle = new Dictionary<int, bool>();

        foreach (int door in currentCycle.Keys)
        {
            if (friendTable[door, end] == true)
                return true;

            for (int i = 0; i < count; i++)
            {
                if (friendTable[door, i] == true)
                {
                    nextCycle[i] = true;
                }
            }
        }
    }

    public static bool HaveConnection(int start, int end)
    {

    }
}
