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
        public static int N = 25, M = 12;
        public static int[] generateLocation = { 0, 4 };
        public static string square = "■";
        public static Stopwatch timeStart = new Stopwatch();
        public static int timeToEnd = 200;
        public static int[,] map = new int[N, M];
        public static ConsoleKeyInfo key;
        public static bool keyPressed = false;
        public static bool action = false;
        public static Block FirstBlock = new Block();
        public static Block NextBlock = new Block();
        public static Block ShiftBlock = new Block();
        public static int numberOfLineClear = 0;
        public static int numberOfLineClearInOneRow = 0;
        public static int lvl = 1;
        public static int point = 0;
        public static bool isCombo = false;
        public static int comboLvl = 0;
        public static int ShiftCounter = 0;
        static void Main(string[] args)
        {
            PrepareMap();
            StartGame();
            GameOver();
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
                    }
                }
                Console.WriteLine();
            }
            //Console.SetWindowSize(50, 30);
        }
        public static void StartGame()
        {
            FirstBlock = GenerateBlock();
            FirstBlock.Prepare();
            NextBlock = GenerateBlock();
            UpdateMap();
            
            while (true)
            {
                timeStart.Start();
                if ((int)timeStart.ElapsedMilliseconds>timeToEnd)//co 0,5 sek generuje nowy klocek
                {
                    if (!FirstBlock.IsSomethingUnder())
                    {
                        Thread.Sleep(timeToEnd);
                        FirstBlock.Drop();
                        action = true;
                        timeStart.Reset();
                    }
                    else
                    {
                        if (map[generateLocation[0], generateLocation[1]] == 2)
                        {
                            //Game Over
                            return;
                        }
                        isCombo = false;
                        ShiftCounter = 0;
                        numberOfLineClearInOneRow = ClearLine();
                        AssignBlock(FirstBlock, NextBlock);
                        FirstBlock.Prepare();
                        NextBlock = GenerateBlock();

                        action = true;
                        //FirstBlock = GenerateBlock();
                        //FirstBlock.Prepare();
                        if (isCombo)
                        {
                            comboLvl++;
                        }
                        if (!isCombo)
                        {
                            comboLvl = 0;
                        }
                    }
                }
                ClickEvent(FirstBlock);
                if (action)
                {
                    UpdateMap();
                    action = false;
                }
                if (comboLvl != 0)
                {
                    point += numberOfLineClearInOneRow * 100 * comboLvl;
                }
                else
                {
                    point += numberOfLineClearInOneRow * 100;
                }
                numberOfLineClearInOneRow = 0;
            }
        }
        public static void GameOver()
        {

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
            ShowNextBlock();
            ShowShiftBlock();
            ShowInfo();
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
                block.LengthAfterRotate();
            }
            if (key.Key == ConsoleKey.Z && keyPressed)
            {
                if (ShiftCounter==0)
                {
                    HideBlock();
                    FirstBlock.Prepare();
                    ShiftCounter++;
                }
            }
        }
        public static int ClearLine()
        {
            int counter;
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
                    numberOfLineClear++;
                    clear = true;
                    if (numberOfLineClear%10==0)
                    {
                        lvl++;
                        timeToEnd -= 50;
                    }
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
                isCombo = true;
            }
            return clearedLineLocal;
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
        public static void AssignBlock(Block target,Block main)
        {
            for (int i = 0; i < main.area.GetLength(0); i++)
            {
                for (int j = 0; j < main.area.GetLength(1); j++)
                {
                    target.area[i, j] = main.area[i, j];
                }
            }
            target.info = main.info;
        }
        public static void ShowNextBlock()
        {
            Console.SetCursorPosition(32, 1);
            Console.Write("Next Block");
            Console.SetCursorPosition(34, 3);
            for (int i = 0; i < NextBlock.area.GetLength(0); i++)
            {
                for (int j = 0; j < NextBlock.area.GetLength(1); j++)
                {
                    if (NextBlock.area[i,j]==1)
                    {
                        Console.Write(square+" ");
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft+2,Console.CursorTop);
                    }
                }
                Console.SetCursorPosition(34, Console.CursorTop+1);
            }
        }
        public static void ShowShiftBlock()
        {
            if (ShiftBlock.info!=null)
            {
                Console.SetCursorPosition(32, 6);
                Console.Write("Shift Block");
                Console.SetCursorPosition(34, 8);
                for (int i = 0; i < NextBlock.area.GetLength(0); i++)
                {
                    for (int j = 0; j < NextBlock.area.GetLength(1); j++)
                    {
                        if (ShiftBlock.area[i, j] == 1)
                        {
                            Console.Write(square + " ");
                        }
                        else
                        {
                            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop);
                        }
                    }
                    Console.SetCursorPosition(34, Console.CursorTop + 1);
                }
            }
            else
            {
                Console.Write("BRAK");
            }
        }
        public static void ShowInfo()
        {
            Console.SetCursorPosition(45, 1);
            Console.Write("Clear Line : " + numberOfLineClear);
            Console.SetCursorPosition(45, 3);
            Console.Write("Lvl : " + lvl);
            Console.SetCursorPosition(45, 5);
            Console.Write("Points : " + point);
            Console.SetCursorPosition(45, 7);
            Console.Write("Combo : " + comboLvl);
        }
        public static void HideBlock()
        {
            if (ShiftBlock.info != null)
            {
                FirstBlock.Clear();
                Block temp = new Block();
                AssignBlock(temp, FirstBlock);
                AssignBlock(FirstBlock, ShiftBlock);
                AssignBlock(ShiftBlock, temp);
            }
            else
            {
                FirstBlock.Clear();
                AssignBlock(ShiftBlock, FirstBlock);
                AssignBlock(FirstBlock, NextBlock);
                NextBlock = GenerateBlock();
            }
        }
    }
}