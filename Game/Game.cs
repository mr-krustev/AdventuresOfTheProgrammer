using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

class Game
{
    public static int gameHeight = 25;
    public static int gameWidth = 50;



    public static int currentLevel = 2;        // Changing this, changes the room (1-2 currently)
    public static bool newLevel = false;

    public static int score = 0;

    public static int reprintWalls = 0;

    static void Main()
    {

        Console.OutputEncoding = Encoding.Unicode;

        Console.WindowHeight = gameHeight + 2;
        Console.BufferHeight = gameHeight + 3;
        Console.WindowWidth = gameWidth + 2;
        Console.BufferWidth = gameWidth + 4;
        Console.CursorVisible = false;

        InfoPanel.Boarders();

        Rooms.FillRoomsWithSymbols(Rooms.room, currentLevel);


        DoTask.PrintArray(InfoPanel.infoPanel, InfoPanel.infoPanelHeight, InfoPanel.infoPanelWidth, Rooms.playFieldWidth);
        DoTask.PrintArray(Rooms.room, Rooms.playFieldHeight, Rooms.playFieldWidth);

        InfoPanel.CurrentStats();
        Hero.GetInitialPosition(currentLevel);

        while (true)
        {
            Hero.PlayerMovement();
            Hero.DrawPlayer(Hero.PositionX, Hero.PositionY);
            InfoPanel.CurrentStats();
            Hero.DrawShots();

            if (newLevel)
            {
                currentLevel++;
                Rooms.FillRoomsWithSymbols(Rooms.room, currentLevel);
            }

            Thread.Sleep(375);







            reprintWalls++;
            if (reprintWalls % 15 == 0)
            {
                Console.Clear();
                InfoPanel.Boarders();
                InfoPanel.PrintRoomName(currentLevel);
                DoTask.PrintArray(InfoPanel.infoPanel, InfoPanel.infoPanelHeight, InfoPanel.infoPanelWidth, Rooms.playFieldWidth);
                DoTask.PrintArray(Rooms.room, Rooms.playFieldHeight, Rooms.playFieldWidth);

            }
        }

    }
}

