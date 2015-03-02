using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InfoPanel
{
    public static int infoPanelHeight = Game.gameHeight;
    public static int infoPanelWidth = Game.gameWidth - Rooms.playFieldWidth;
    public static int[,] infoPanel = new int[infoPanelHeight, infoPanelWidth];


    public static void Boarders()
    {
        DoTask.FillArray(1, infoPanelHeight, 0, 0, infoPanel);                        //left
        DoTask.FillArray(2, infoPanelWidth - 1, 0, 1, infoPanel);                     //top
        DoTask.FillArray(1, infoPanelHeight, 0, infoPanelWidth - 1, infoPanel);       //right
        DoTask.FillArray(2, infoPanelWidth - 2, infoPanelHeight - 1, 1, infoPanel);   //bottom
    }

    public static void CurrentStats()
    {
        PrintRoomName(Game.currentLevel);

        DoTask.Print(3, Rooms.playFieldWidth + (infoPanelWidth - "Level".Length) / 2, "Level:");
        DoTask.Print(4, Rooms.playFieldWidth + (infoPanelWidth - Game.currentLevel.ToString().Length) / 2, Game.currentLevel);
        DoTask.Print(7, Rooms.playFieldWidth + (infoPanelWidth - "Score".Length) / 2, "Score:");
        int scorePosition = (infoPanelWidth - Game.score.ToString().Length) / 2;
        DoTask.Print(8, Rooms.playFieldWidth + scorePosition, Game.score);
        DoTask.Print(11, Rooms.playFieldWidth + (infoPanelWidth - "Artifacts".Length) / 2, "Artifacts:");
        //DoTask.print(12,Rooms.playFieldWidth + 4, Artifacts.artifacts);


        DoTask.Print(15, Rooms.playFieldWidth + (infoPanelWidth - "High Scores".Length) / 2, "High Scores:");

    }

    public static void PrintRoomName(int currentLevel)
    {
        string text = "";
        if (currentLevel == 1)
        {
            text = "Entry Room";
            DoTask.Print(1, Rooms.playFieldWidth + (infoPanelWidth - text.Length) / 2, text);
        }
        else if (currentLevel == 2)
        {
            text = "Mirror Room";
            DoTask.Print(1, Rooms.playFieldWidth + (infoPanelWidth - text.Length) / 2, text);
        }

    }
}

