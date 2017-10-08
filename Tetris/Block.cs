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
        public static string info;
        public static int[] lastLocation = new int[2]; 
        public static bool droped = false;

        //int[,] I = new int[4, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] O = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] J = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] L = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] T = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] S = new int[4, 4] { { 0, 1, 1, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        //int[,] Z = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

        #region Shape
        int[,] I_1 = new int[4, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] I_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 } };
        int[,] O_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] L_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
        int[,] L_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 1, 1, 1 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] L_3 = new int[4, 4] { { 0, 1, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 0 } };
        int[,] L_4 = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 1 }, { 0, 1, 1, 1 }, { 0, 0, 0, 0 } };
        int[,] J_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] J_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 } };
        int[,] J_3 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] J_4 = new int[4, 4] { { 1, 1, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T_2 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T_3 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] T_4 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] S_1 = new int[4, 4] { { 0, 1, 1, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] S_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] Z_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int[,] Z_2 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
        #endregion

        public Block(int seed)
        {
            Random rand = new Random(seed);
            area = new int[4, 4];
            List<int[,]> shapeList = new List<int[,]>() { I_1, O_1, J_1, L_1, T_1, S_1, Z_1 };
            List<string> shapeInfo = new List<string>() { "I_1", "0_2", "J_1", "L_1", "T_1", "S_1", "Z_1" };
            int fate = rand.Next(0, 7);
            SwitchArea(shapeList[fate]);
            info = shapeInfo[fate];
            Prepare();
            
        }
        public Block(string id)
        {
            area = new int[4, 4];
            List<int[,]> shapeList = new List<int[,]>() { I_1, O_1, J_1, L_1, T_1, S_1, Z_1 };
            List<string> shapeInfo = new List<string>() { "I_1", "0_2", "J_1", "L_1", "T_1", "S_1", "Z_1" };
            SwitchArea(shapeList[Int32.Parse(id)]);
            info = shapeInfo[Int32.Parse(id)];
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
                    }
                }
            }
            switch (info)
            {
                case "I_1":
                    {
                        SwitchArea(I_2);
                        info = "I_2";
                        break;
                    }
                case "I_2":
                    {
                        SwitchArea(I_1);
                        info = "I_1";
                        break;
                    }
                case "O_1":
                    {
                        break;
                    }
                case "J_1":
                    {
                        SwitchArea(J_2);
                        info = "J_2";
                        break;
                    }
                case "J_2":
                    {
                        SwitchArea(J_3);
                        info = "J_3";
                        break;
                    }
                case "J_3":
                    {
                        SwitchArea(J_4);
                        info = "J_4";
                        break;
                    }
                case "J_4":
                    {
                        SwitchArea(J_1);
                        info = "J_1";
                        break;
                    }
                case "L_1":
                    {
                        SwitchArea(L_2);
                        info = "L_2";
                        break;
                    }
                case "L_2":
                    {
                        SwitchArea(L_3);
                        info = "L_3";
                        break;
                    }
                case "L_3":
                    {
                        SwitchArea(L_4);
                        info = "L_4";
                        break;
                    }
                case "L_4":
                    {
                        SwitchArea(L_1);
                        info = "L_1";
                        break;
                    }
                case "T_1":
                    {
                        SwitchArea(T_2);
                        info = "T_2";
                        break;
                    }
                case "T_2":
                    {
                        SwitchArea(T_3);
                        info = "T_3";
                        break;
                    }
                case "T_3":
                    {
                        SwitchArea(T_4);
                        info = "T_4";
                        break;
                    }
                case "T_4":
                    {
                        SwitchArea(T_1);
                        info = "T_1";
                        break;
                    }
                case "S_1":
                    {
                        SwitchArea(S_2);
                        info = "S_2";
                        break;
                    }
                case "S_2":
                    {
                        SwitchArea(S_1);
                        info = "S_1";
                        break;
                    }
                case "Z_1":
                    {
                        SwitchArea(Z_2);
                        info = "Z_2";
                        break;
                    }
                case "Z_2":
                    {
                        SwitchArea(Z_1);
                        info = "Z_1";
                        break;
                    }
                default:
                    break;
            }
            for (int i = lastLocation[0]; i < area.GetLength(0) + lastLocation[0]; i++)
            {
                for (int j = lastLocation[1]; j < area.GetLength(1) + lastLocation[1]; j++)
                {
                    if (area[i - lastLocation[0], j - lastLocation[1]] == 1)
                    {
                        Program.map[i, j] = 1;
                    }
                }
            }
        }
        public void SwitchArea(int[,] featureShape)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    area[i, j] = featureShape[i, j];
                }
            }
        }

    }
}       
