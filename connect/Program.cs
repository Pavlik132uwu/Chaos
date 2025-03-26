using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[,] board = new int[6, 7]{
            //    { 0, 0, 0, 0, 1, 0, 0 },
            //    { 0, 0, 0, 1, 2, 0, 0 },
            //    { 0, 0, 1, 1, 1, 0, 0 },
            //    { 0, 1, 1, 1, 2, 2, 2 },
            //    { 1, 1, 2, 1, 2, 2, 2 },
            //    { 1, 2, 2, 1, 2, 2, 2 }};
            //int[] position = { 0, 4 };
            //// pozice řádek s indexem 0
            //// pozice sloupec s indexem 4
            //if (CheckWin(board, 5, 1, position)) //nutno změnit tu 3 proměnou za hráče (1 nebo 2)
            //{
            //    Console.WriteLine("Vyhrál jsi");
            //}

            int pocetHrac = 2;
            int neededToWin = 5;
            int vyska = 7;
            int sirka = 7;
            Hra hra1 = new Hra(vyska, sirka, neededToWin, pocetHrac)

        }
    }

    class Hra
    {
        // toto dát s mými proměnými a tak

        //public Hra(int pocetVyhernichZetonu, int sirkaPole, int vyskaPole, int pocetHracu)
        //{
        //    this.pocetVyhernichZetonu = pocetVyhernichZetonu;
        //    hraciPole = new int[sirkaPole, vyskaPole];
        //    hraci = new Hrac[pocetHracu];
        //}

        //int pocetVyhernichZetonu; // datová položka
        //int[,] hraciPole;

        //Hrac[] hraci;
        //public Hrac Play()
        //{
        //    Hrac hrac = new Hrac();
        //    Position startPosition = new Position();
        //    Console.WriteLine("Na řadě je hráč", hrac.Jmeno);
        //    startPosition.Row = 0;
        //    startPosition.Column = 0;


        //    // Tah
        //    // Check
        //    //střídání hračů
        //}


        public static bool CheckWin(int[,] board, int neededToWin, int hrac, int[] soucasnaPozice)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            int x = soucasnaPozice[0];
            int y = soucasnaPozice[1];

            // Kontrola řádku -
            if (CheckDirection(board, neededToWin, hrac, x, y, 0, 1)) return true;

            // Kontrola sloupce | 
            if (CheckDirection(board, neededToWin, hrac, x, y, 1, 0)) return true;

            // Kontrola diagonály \
            if (CheckDirection(board, neededToWin, hrac, x, y, 1, 1)) return true;

            // Kontrola diagonály /
            if (CheckDirection(board, neededToWin, hrac, x, y, 1, -1)) return true;

            return false;
        }

        private static bool CheckDirection(int[,] board, int neededToWin, int hrac, int r, int c, int dr, int dc)
        {
            int count = 1;
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // Prohledání jedním směrem
            for (int i = 1; i < neededToWin; i++)
            {
                int nr = r + dr * i;
                int nc = c + dc * i;
                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || board[nr, nc] != hrac)
                    break;
                count++;
            }

            // Prohledání opačným směrem
            for (int i = 1; i < neededToWin; i++)
            {
                int nr = r - dr * i;
                int nc = c - dc * i;
                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || board[nr, nc] != hrac)
                    break;
                count++;
            }

            return count >= neededToWin; //tohle mi bude dávat true nebo false
        }
    }
    //taky předělat na moje

    //class Hrac
    //{
    //    public string Jmeno;
    //    public string Symbol;
    //}
    //struct Position
    //{
    //    public int Row;
    //    public int Column;
    //}
}