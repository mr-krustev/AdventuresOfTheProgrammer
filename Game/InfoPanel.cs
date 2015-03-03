using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InfoPanel
{
    public static int infoPanelHeight = Game.gameHeight;
    public static int infoPanelWidth = Game.gameWidth - Rooms.playFieldWidth;
    public static int[,] infoPanel = new int[infoPanelHeight, infoPanelWidth + 1];


    public static void Boarders()
    {
        DoTask.FillArray(1, infoPanelHeight, 0, 0, infoPanel);                        //left
        DoTask.FillArray(2, infoPanelWidth - 1, 0, 1, infoPanel);                     //top
        DoTask.FillArray(1, infoPanelHeight, 0, infoPanelWidth - 1, infoPanel);       //right
        DoTask.FillArray(2, infoPanelWidth - 2, infoPanelHeight - 1, 1, infoPanel);   //bottom
    }

    public static void CurrentStats()
    {


        DoTask.Print(3, Rooms.playFieldWidth + (infoPanelWidth - "Level".Length) / 2, "Level:");
        DoTask.Print(4, Rooms.playFieldWidth + (infoPanelWidth - Game.currentLevel.ToString().Length) / 2, Game.currentLevel);
        DoTask.Print(7, Rooms.playFieldWidth + (infoPanelWidth - "Score".Length) / 2, "Score:");

        DoTask.Print(11, Rooms.playFieldWidth + (infoPanelWidth - "Lives".Length) / 2, "Lives:");
        

        DoTask.Print(14, Rooms.playFieldWidth + (infoPanelWidth - "Hero name".Length) / 2, "Hero name:");
        DoTask.Print(15, Rooms.playFieldWidth + (infoPanelWidth - Game.heroName.Length) / 2, Game.heroName);

        DoTask.Print(17, Rooms.playFieldWidth + (infoPanelWidth - "High Scores".Length) / 2, "High Scores:");
        DoTask.Print(18, Rooms.playFieldWidth + (infoPanelWidth - Game.topFiveScores[0].Length - 1) / 2, Game.topFiveScores[0]);
        DoTask.Print(19, Rooms.playFieldWidth + (infoPanelWidth - Game.topFiveScores[1].Length - 1) / 2, Game.topFiveScores[1]);
        DoTask.Print(20, Rooms.playFieldWidth + (infoPanelWidth - Game.topFiveScores[2].Length - 1) / 2, Game.topFiveScores[2]);
        DoTask.Print(21, Rooms.playFieldWidth + (infoPanelWidth - Game.topFiveScores[3].Length - 1) / 2, Game.topFiveScores[3]);
        DoTask.Print(22, Rooms.playFieldWidth + (infoPanelWidth - Game.topFiveScores[4].Length - 1) / 2, Game.topFiveScores[4]);

    }

    public static void CurrentScore()
    {
        int scorePosition = (infoPanelWidth - Game.score.ToString().Length) / 2;
        DoTask.Print(8, Rooms.playFieldWidth + scorePosition, Game.score);
        DoTask.Print(12, Rooms.playFieldWidth + (infoPanelWidth - Hero.lives.ToString().Length) / 2, Hero.lives);
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

