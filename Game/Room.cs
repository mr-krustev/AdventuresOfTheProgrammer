using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Rooms
{
    
    public static int playFieldHeight = Game.gameHeight - 10;
    public static int playFieldWidth = Game.gameWidth - 15;

    public static int[,] room = new int[playFieldHeight + 1, playFieldWidth+1];

    public static void FillRoomsWithSymbols(int[,] room, int Level)
    {
        //Cleans the playfield
        for (int i = 1; i < playFieldHeight; i++)
        {
            for (int j = 1; j < playFieldWidth - 1; j++)
            {
                room[i, j] = 0;
            }
        }

        //Room walls
        DoTask.FillArray(1, playFieldHeight, 0, 0, room);       //left
        DoTask.FillArray(2, playFieldWidth - 1, 0, 0, room);    //top
        DoTask.FillArray(1, playFieldHeight, 0, playFieldWidth - 1, room);      //right
        DoTask.FillArray(2, playFieldWidth - 1, playFieldHeight - 1, 0, room);    //bottom

        if (Level == 1)
        {
            // Top-Left corner
            DoTask.FillArray(1, 6, 1, 4, room);
            DoTask.FillArray(1, 6, 2, 10, room);

            // Top-Mid
            DoTask.FillArray(2, 6, 2, 11, room);    //line left '-'          
            DoTask.FillArray(1, 6, 4, 13, room);
            DoTask.FillArray(2, 10, 4, 16, room);   //long line '-'

            DoTask.FillArray(1, 4, 5, 17, room);
            DoTask.FillArray(1, 3, 1, 21, room);    // separating line '|'
            DoTask.FillArray(1, 3, 5, 25, room);

            // Top-Right
            DoTask.FillArray(1, 6, 3, 28, room);    //Line '|'

            DoTask.FillArray(2, 3, 3, 31, room);
            DoTask.FillArray(2, 3, 5, 29, room);    //Zig-zag
            DoTask.FillArray(2, 3, 7, 31, room);

            //Bottom-Left
            DoTask.FillArray(2, 10, 8, 1, room);    //upper line '-'    
            DoTask.FillArray(2, 10, 12, 2, room);   //bottom line '-'

            DoTask.FillArray(1, 2, 9, 3, room);
            DoTask.FillArray(1, 2, 10, 6, room);    //Zig-zag '|'
            DoTask.FillArray(1, 2, 9, 9, room);

            //Bottom-Mid
            DoTask.FillArray(1, 6, 8, 22, room);    //right line '|'

            DoTask.FillArray(2, 10, 10, 12, room);  //top line '-'

            DoTask.FillArray(1, 2, 11, 12, room);
            DoTask.FillArray(1, 2, 12, 15, room);   //Middle-Bottom, Zig-zag
            DoTask.FillArray(1, 2, 11, 18, room);

            //Bottom-Right
            DoTask.FillArray(2, 9, 9, 23, room);    //top line '-'

            DoTask.FillArray(1, 3, 11, 28, room);   //Zig-Zag '|'
            DoTask.FillArray(1, 3, 10, 31, room);

        }
        else if (Level == 2)
        {
            //Top-Left
            DoTask.FillArray(2, 13, 3, 4, room);    //  '-'
            DoTask.FillArray(2, 11, 5, 3, room);    //  '-'

            //Top-Mid
            DoTask.FillArray(1, 3, 5, 14, room);
            DoTask.FillArray(1, 6, 1, 17, room);    //Turn '|'
            DoTask.FillArray(1, 5, 3, 20, room);

            DoTask.FillArray(2, 3, 5, 22, room);    //Right Zig-zag '-'
            DoTask.FillArray(2, 3, 3, 21, room);    //Right Zig-zag '-'

            //Top-Right
            DoTask.FillArray(1, 5, 1, 25, room);
            DoTask.FillArray(1, 4, 1, 28, room);    //Turn '|'
            DoTask.FillArray(1, 4, 2, 31, room);

            DoTask.FillArray(2, 7, 6, 25, room);    //Bottom line '-'

            //Bottom-Left
            DoTask.FillArray(1, 6, 6, 3, room);     //Turn '|'

            DoTask.FillArray(1, 7, 7, 8, room);     //Zig-Zag wall, '|'

            DoTask.FillArray(2, 3, 7, 11, room);
            DoTask.FillArray(2, 3, 9, 9, room);
            DoTask.FillArray(2, 3, 11, 11, room);

            //Bottom-Mid
            DoTask.FillArray(2, 20, 8, 14, room);   //Bottom long top line '-'

            DoTask.FillArray(1, 4, 9, 14, room);    //Left wall '|'

            //First hiding place
            DoTask.FillArray(1, 1, 13, 16, room);
            DoTask.FillArray(2, 1, 12, 16, room);
            DoTask.FillArray(1, 1, 11, 17, room);
            DoTask.FillArray(2, 1, 10, 16, room);

            DoTask.FillArray(1, 4, 9, 19, room);    //Middle wall(left)  '|'
            DoTask.FillArray(1, 4, 10, 21, room);   //Middle wall(right) '|'

            //Second hiding place
            DoTask.FillArray(1, 1, 9, 23, room);
            DoTask.FillArray(2, 1, 12, 23, room);
            DoTask.FillArray(1, 1, 11, 24, room);
            DoTask.FillArray(2, 1, 10, 23, room);

            //Bottom-Right            
            DoTask.FillArray(1, 4, 10, 26, room);   //Left wall '|'
            DoTask.FillArray(2, 6, 10, 27, room);   //Upper wall '-'


        }

    } // Fills the matrix with numbers => they represent later walls.

    

}