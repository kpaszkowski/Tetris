using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Tetris
{
    class Program
    {
        public static int N=25, M=12;
        public static int[] generateLocation = { 0 , 4 };
        public static string square = "■";
        public static Stopwatch timeStart = new Stopwatch();
        public static int timeToEnd = 200;
        public static int[,] map = new int[N, M];
        public static ConsoleKeyInfo key;
        public static bool keyPressed = false;
        public static bool action = false;
        public static int clearedLineGlobal = 0;
        static void Main(string[] args)
        {
            PrepareMap();
            StartGame();
            Console.ReadLine();
        }
        public static void PrepareMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i==N-1 || j==0 || j==M-1)
                    {
                        map[i, j] = -1;
                        //Console.Write("* ");
                    }
                    else
                    {
                        //Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void StartGame()
        {
            UpdateMap();
            Block block = GenerateBlock();
            while (true)
            {
                timeStart.Start();
                if ((int)timeStart.ElapsedMilliseconds>timeToEnd)//co 0,5 sek generuje nowy klocek
                {
                    if (!block.IsSomethingUnder())
                    {
                        Thread.Sleep(timeToEnd);
                        block.Drop();
                        action = true;
                        timeStart.Reset();
                        //
                    }
                    else
                    {
                        ClearLine();
                        block = GenerateBlock();
                    }
                }
                ClickEvent(block);
                if (action)
                {
                    UpdateMap();
                    action = false;
                }
            }

        }
        public static void UpdateMap()
        {
            Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j]==-1)
                    {
                        Console.Write("* ");
                    }
                    else if (map[i,j]==1)
                    {
                        Console.Write(square+" ");
                    }
                    else if (map[i,j]==2)
                    {
                        Console.Write(square+" ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
        public static Block GenerateBlock()
        {
            Random rand = new Random();
            Block block = new Block(rand.Next());
            return block;
        }
        public static void ClickEvent(Block block)
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                keyPressed = true;
                action = true;
            }
            else
            {
                keyPressed = false;
            }
            if (key.Key==ConsoleKey.RightArrow && keyPressed && !block.IsSomethingRight())
            {
                block.MoveRight();
            }
            if (key.Key == ConsoleKey.LeftArrow && keyPressed && !block.IsSomethingLeft())
            {
                block.MoveLeft();
            }
            if (key.Key == ConsoleKey.P && keyPressed)
            {
                Console.ReadKey();
            }
            if (key.Key == ConsoleKey.DownArrow && keyPressed)
            {
                block.MoveDown();
            }
            if (key.Key == ConsoleKey.Spacebar && keyPressed)
            {
                block.MoveFastDown();
            }
            if (key.Key == ConsoleKey.UpArrow && keyPressed && !block.IsSomethingUnder())
            {
                block.RotateRight();
            }
        }
        public static void ClearLine()
        {
            int counter;
            int target = 0;
            bool clear = false;
            int line1 = -1;
            int line2 = -1;
            int line3 = -1;
            int line4 = -1;
            int clearedLineLocal = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                counter = 0;
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j]==2)
                    {
                        counter++;
                    }
                }
                if (counter==10)
                {
                    for (int j = 1; j < map.GetLength(1)-1; j++)
                    {
                        map[i, j] = 0;
                    }
                    if (line1 == -1)
                    {
                        line1 = i;
                    }
                    else if (line2 == -1)
                    {
                        line2 = i;
                    }
                    else if (line3 == -1)
                    {
                        line3 = i;
                    }
                    else if (line4==-1)
                    {
                        line4 = i;
                    }
                    clearedLineLocal++;
                    clearedLineGlobal++;
                    clear = true;
                }
            }
            //opuszczanie reszty klocków
            if (clear)
            {
                for (int num = 0; num < clearedLineLocal; num++)
                {
                    if (line1!=-1)
                    {
                        ClearAbove(line1);
                        line1 = -1;
                    }
                    else if (line2!=-1)
                    {
                        ClearAbove(line2);
                        line2 = -1;
                    }
                    else if (line3 != -1)
                    {
                        ClearAbove(line3);
                        line3 = -1;
                    }
                    else if (line4 != -1)
                    {
                        ClearAbove(line4);
                        line4 = -1;
                    }
                }
            }
        }
        public static void Assign(int[,]target ,int[,]main)
        {
            for (int i = 0; i < main.GetLength(0); i++)
            {
                for (int j = 0; j < main.GetLength(1); j++)
                {
                    target[i, j] = main[i, j];
                }
            }
        }
        public static void ClearAbove(int line)
        {
            int[,] wholeArea = new int[map.GetLength(0), map.GetLength(1)];
            Assign(wholeArea, map);
            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j]==2)
                    {
                        map[i, j] = 0;
                    }
                }
            }
            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (wholeArea[i,j]==2)
                    {
                        map[i + 1, j] = 2;
                    }
                }
            }

        }
    }
}