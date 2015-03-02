using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DoTask
{

    public static void FillArray(int type, int length, int row, int col, int[,] room) //Method used to fill matrix. Type '1' == '|', Type '2' == '-'
    {
        if (type == 1)
        {
            for (int i = 0; i < length; i++)
            {
                room[row + i, col] = 1;
            }
        }
        else if (type == 2)
        {
            for (int i = 0; i < length; i++)
            {
                room[row, col + i] = 2;
            }
        }
    }

    public static void Print(int col, int row, object data) // Method used for printing.
    {
        Console.SetCursorPosition(row, col);
        Console.Write(data);
    }

    public static void PrintArray(int[,] array, int maxRow, int maxCol, int widthPos = 0)
    {
        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxCol; j++)
            {
                switch (array[i, j])
                {
                    case 1:
                        Print(i, j + widthPos, '|');
                        break;

                    case 2:
                        Print(i, j + widthPos, '-');
                        break;

                    default:
                        Print(i, j + widthPos, ' ');
                        break;
                }
            }
        }
    } // Using the numbers in the matrix, prints the gamefield

    public static bool CheckIfWall(int posY, int posX)
    {
        return (Rooms.room[posY, posX] == 1 || Rooms.room[posY, posX] == 2);
    }

}

