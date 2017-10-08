using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public static int[,] area;
        public static int[] lastLocation = new int[2]; 
        public static bool droped = false;
        int[,] I = new int[4, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] O = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] J = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] L = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] S = new int[4, 4] { { 0, 1, 1, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] Z = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

        public Block(int seed)
        {
            Random rand = new Random(seed);
            List<int[,]> shapeList = new List<int[,]>() { I, O, J, L, T, S, Z };
            area = shapeList[rand.Next(0, 7)];
            Prepare();
            
        }
        public Block(string id)
        {
            List<int[,]> shapeList = new List<int[,]>() { I, O, J, L, T, S, Z };
            area = shapeList[Int32.Parse(id)];
            Prepare();
        }
        public static void Prepare()
        {
            //umiescic na mapie klocek
            int locationX = Program.generateLocation[1];
            int locationY = Program.generateLocation[0];
            lastLocation[1] = locationX;
            lastLocation[0] = locationY;
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    if (area[i,j]==1)
                    {
                        Program.map[locationY, locationX] = 1;
                    }
                    locationX += 1;
                }
                locationY += 1;
                locationX -= area.GetLength(1);

            }
        }
        public void Drop()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0)+lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1)+lastLocation[1]; j++)
                {
                    if (area[i-lastLocation[0],j-lastLocation[1]]==1)
                    {
                        Program.map[i, j] = 0;
                        //Program.map[i + 1, j] = 1;
                    }
                }
            }
            lastLocation[0] += 1;
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        //Program.map[i, j] = 0;
                        Program.map[i , j] = 1;
                    }
                }
            }
            //lastLocation[1] = 1;
        }
        public void MoveLeft()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        Program.map[i, j] = 0;
                        //Program.map[i + 1, j] = 1;
                    }
                }
            }
            lastLocation[1] -= 1;
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        //Program.map[i, j] = 0;
                        Program.map[i, j] = 1;
                    }
                }
            }
        }
        public void MoveRight()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        Program.map[i, j] = 0;
                        //Program.map[i + 1, j] = 1;
                    }
                }
            }
            lastLocation[1] += 1;
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        //Program.map[i, j] = 0;
                        Program.map[i, j] = 1;
                    }
                }
            }
        }
        public void MoveDown()
        {
            if (!IsSomethingUnder())
            {
                Drop();
            }
        }
        public void MoveFastDown()
        {
            for (int i = 0; i < 50; i++)
            {
                if (!IsSomethingUnder())
                {
                    Drop();
                }
            }
        }
        public bool IsSomethingUnder()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        if (Program.map[i + 1, j] == -1 || Program.map[i + 1, j] == 2 )
                        {
                            //zmienianie ustawionego bloku
                            for (int x = lastLocation[0]; x < area.GetLength(0) + lastLocation[0]; x++)
                            {
                                for (int y = lastLocation[1]; y < area.GetLength(1) + lastLocation[1]; y++)
                                {
                                    if (area[x - lastLocation[0], y - lastLocation[1]] == 1)
                                    {
                                        //Program.map[i, j] = 0;
                                        Program.map[x, y] = 2;
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsSomethingRight()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        if (Program.map[i , j+1] == -1 || Program.map[i , j+1] == 2 )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool IsSomethingLeft()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        if (Program.map[i, j - 1] == -1 || Program.map[i, j - 1] == 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void RotateRight()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        Program.map[i, j] = 0;
                        //Program.map[i + 1, j] = 1;
                    }
                }
            }
        }
        //update?
        public void UpdateBlock()
        {
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        //update
                        Console.SetCursorPosition(j, i);
                        Console.Write("*");
                    }
                }
            }
        }
    }
}


/*
       #region Shape
       int[,] I_1 = new int[4, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] I_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 } };
       int[,] O_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] L_1 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] L_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] L_3 = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] L_4 = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 1, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 } };
       int[,] J_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] J_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 } };
       int[,] J_3 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] J_4 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 0 } };
       int[,] T_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] T_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] T_3 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] T_4 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] S_1 = new int[4, 4] { { 0, 1, 1, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] S_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] Z_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
       int[,] Z_2 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
       #endregion
       */
