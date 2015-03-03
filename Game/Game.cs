using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

class Game
{
    public static int gameHeight = 25;
    public static int gameWidth = 50;

    // Changing this, changes the room (1-2 currently)
    public static int currentLevel = 2;
    public static bool newLevel = false;
    public static bool getArtefact = false;

    public static int score = 1000;

    public static int reprintWalls = 0;
    public static string heroName = "";

    static bool isGameRunning;
    static void Main()
    {

        Console.OutputEncoding = Encoding.Unicode;

        Console.WindowHeight = gameHeight + 2;
        Console.BufferHeight = gameHeight + 3;
        Console.WindowWidth = gameWidth + 2;
        Console.BufferWidth = gameWidth + 4;
        Console.CursorVisible = false;

        //Entering username


        while (heroName.Length > 3 || heroName.Length == 0 && !heroName.Contains('-'))
        {
            DoTask.Print(7, 8, "Hello, soldier");
            DoTask.Print(8, 3, "You have been chosen for this quest.");
            DoTask.Print(9, 1, "Please enter your initials below.");
            DoTask.Print(10, 5, "<up to 3 symbols>: ");
            heroName = Console.ReadLine();
            Console.Clear();
        }

        InfoPanel.Boarders();

        Rooms.FillRoomsWithSymbols(Rooms.room, currentLevel);


        DoTask.PrintArray(InfoPanel.infoPanel, InfoPanel.infoPanelHeight, InfoPanel.infoPanelWidth, Rooms.playFieldWidth);
        DoTask.PrintArray(Rooms.room, Rooms.playFieldHeight, Rooms.playFieldWidth);

        InfoPanel.CurrentStats();
        Hero.GetInitialPosition(currentLevel);
        Traps.TrapsPerLevel(currentLevel);
        Monsters.MonsterPerLevel(currentLevel);
        while (true)
        {
            Hero.PlayerMovement();
            Hero.DrawPlayer(Hero.PositionX, Hero.PositionY);
            InfoPanel.CurrentStats();
            Hero.DrawShots();
            if (Hero.lives == 0)
            {
                Console.Clear();

                // Add end-game stuff;



                break;
            }
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

