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
        public static int timeToEnd = 300;
        public static int[,] map = new int[N, M];
        public static ConsoleKeyInfo key;
        public static bool keyPressed = false;
        public static bool action = false;
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
                        block.UpdateBlock();
                        timeStart.Reset();
                    }
                    else
                    {
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
            if (key.Key == ConsoleKey.UpArrow && keyPressed)
            {
                block.RotateRight();
            }
            if (key.Key == ConsoleKey.D && keyPressed)
            {
                block.RotateRight();
            }
        }
    }
}
