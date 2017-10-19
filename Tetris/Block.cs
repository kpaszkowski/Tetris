using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public int[,] area { get; set; }
        public string info { get; set; }
        public int[] lastLocation { get; set; } = new int[2];
        public bool droped { get; set; } = false;
        #region Shape
        private int[,] I_1 = new int[4, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] I_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 } };
        private int[,] O_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] L_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
        private int[,] L_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 1, 1, 1 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] L_3 = new int[4, 4] { { 0, 1, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 0 } };
        private int[,] L_4 = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 1 }, { 0, 1, 1, 1 }, { 0, 0, 0, 0 } };
        private int[,] J_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] J_2 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 } };
        private int[,] J_3 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] J_4 = new int[4, 4] { { 1, 1, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] T_1 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] T_2 = new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] T_3 = new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] T_4 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] S_1 = new int[4, 4] { { 0, 1, 1, 0 }, { 1, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] S_2 = new int[4, 4] { { 1, 0, 0, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] Z_1 = new int[4, 4] { { 1, 1, 0, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private int[,] Z_2 = new int[4, 4] { { 0, 1, 0, 0 }, { 1, 1, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 0, 0 } };
        #endregion

        public Block()
        {
            area = new int[4, 4];
        }
        public Block(int seed)
        {
            Random rand = new Random(seed);
            area = new int[4, 4];
            List<int[,]> shapeList = new List<int[,]>() { I_1, O_1, J_1, L_1, T_1, S_1, Z_1 };
            List<string> shapeInfo = new List<string>() { "I_1", "O_1", "J_1", "L_1", "T_1", "S_1", "Z_1" };
            int fate = rand.Next(0, 7);
            SwitchArea(shapeList[fate]);
            info = shapeInfo[fate];
            
        }
        public Block(string id)
        {
            area = new int[4, 4];
            List<int[,]> shapeList = new List<int[,]>() { I_1, O_1, J_1, L_1, T_1, S_1, Z_1 };
            List<string> shapeInfo = new List<string>() { "I_1", "O_1", "J_1", "L_1", "T_1", "S_1", "Z_1" };
            SwitchArea(shapeList[Int32.Parse(id)]);
            info = shapeInfo[Int32.Parse(id)];
        }
        public void Prepare()
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
            Clear();
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
                        /**/if (Program.map[i , j+1] == -1 || Program.map[i , j+1] == 2 )
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
        public void Rotate(int[,] featureBlock, string featureInfo)
        {
            Clear();
            SwitchArea(featureBlock);
            info = featureInfo;
            Draw();
        }
        public void CalculateDiv()
        {
            int div = 0;
            div = lastLocation[1] + CheckLength(I_1) - Program.M;
            if (div >= 0)
            {
                Clear();
                lastLocation[1] -= (div + 1);
            }
        }
        public void LengthAfterRotate()
        {
            int[,] featureArea = new int[4, 4];
            string featureInfo = "";
            switch (info)
            {
                case "I_1":
                    {
                        featureArea = I_2;
                        featureInfo = "I_2";
                        CalculateDiv();
                        break;
                    }
                case "I_2":
                    {
                        featureArea = I_1;
                        featureInfo = "I_1";
                        CalculateDiv();
                        break;
                    }
                case "O_1":
                    {
                        featureArea = O_1;
                        featureInfo = "O_1";
                        CalculateDiv();
                        break;
                    }
                case "J_1":
                    {
                        featureArea = J_2;
                        featureInfo = "J_2";
                        CalculateDiv();
                        break;
                    }
                case "J_2":
                    {
                        featureArea = J_3;
                        featureInfo = "J_3";
                        CalculateDiv();
                        break;
                    }
                case "J_3":
                    {
                        featureArea = J_4;
                        featureInfo = "J_4";
                        CalculateDiv();
                        break;
                    }
                case "J_4":
                    {
                        featureArea = J_1;
                        featureInfo = "J_1";
                        CalculateDiv();
                        break;
                    }
                case "L_1":
                    {
                        featureArea = L_2;
                        featureInfo = "L_2";
                        CalculateDiv();
                        break;
                    }
                case "L_2":
                    {
                        featureArea = L_3;
                        featureInfo = "L_3";
                        CalculateDiv();
                        break;
                    }
                case "L_3":
                    {
                        featureArea = L_4;
                        featureInfo = "L_4";
                        CalculateDiv();
                        break;
                    }
                case "L_4":
                    {
                        featureArea = L_1;
                        featureInfo = "L_1";
                        CalculateDiv();
                        break;
                    }
                case "T_1":
                    {
                        featureArea = T_2;
                        featureInfo = "T_2";
                        CalculateDiv();
                        break;
                    }
                case "T_2":
                    {
                        featureArea = T_3;
                        featureInfo = "T_3";
                        CalculateDiv();
                        break;
                    }
                case "T_3":
                    {
                        featureArea = T_4;
                        featureInfo = "T_4";
                        CalculateDiv();
                        break;
                    }
                case "T_4":
                    {
                        featureArea = T_1;
                        featureInfo = "T_1";
                        CalculateDiv();
                        break;
                    }
                case "S_1":
                    {
                        featureArea = S_2;
                        featureInfo = "S_2";
                        CalculateDiv();
                        break;
                    }
                case "S_2":
                    {
                        featureArea = S_1;
                        featureInfo = "S_1";
                        CalculateDiv();
                        break;
                    }
                case "Z_1":
                    {
                        featureArea = Z_2;
                        featureInfo = "Z_2";
                        CalculateDiv();
                        break;
                    }
                case "Z_2":
                    {
                        featureArea = Z_1;
                        featureInfo = "Z_1";
                        CalculateDiv();
                        break;
                    }
                default:
                    break;
            }
            Rotate(featureArea, featureInfo);
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
        public void Clear()
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
        }
        public void Draw()
        {
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
        public int CheckLength(int[,] shape)
        {
            int counter = 0;
            int counterMax = 0;
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == 1)
                    {
                        counter++;
                    }
                }
                if (counter > counterMax)
                {
                    counterMax = counter;
                }
                counter = 0;
            }
            return counterMax;
        }
    }
}       
