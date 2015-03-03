using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;


public class SteppingOnWallException : ApplicationException
{
}

public class Shots
{
    public int x { get; set; }
    public int y { get; set; }




}



public class Hero
{
    public static bool shotIsNotActive = true;
    public static int lives = 2;
    public static char shotSymbol = '*';
    public static char symbol;
    public static string direction = "";

    public static List<List<int>> shots = new List<List<int>>();
    public static List<string> shotDirection = new List<string>();

    public static Shots currentShot = new Shots();



    public static int PositionY { get; set; }
    public static int PositionX { get; set; }

    public static void GetInitialPosition(int currentLevel)
    {
        if (currentLevel == 1)
        {
            PositionY = 1;
            PositionX = 1;
            direction = "right";
        }
        else if (currentLevel == 2)
        {
            PositionY = 1;
            PositionX = 26;
            direction = "down";
        }

    }


    public static void DrawPlayer(int PositionY, int PositionX)
    {

        if (direction == "left")
        {
            symbol = '<';
        }
        if (direction == "right")
        {
            symbol = '>';
        }
        if (direction == "up")
        {
            symbol = '^';
        }
        if (direction == "down")
        {
            symbol = 'v';
        }
        DoTask.Print(PositionX, PositionY, symbol);

    }

    public static void Shoot()
    {

        {
            if (shotDirection.Count == 0 || currentShot.x == 0)
            {
                shotIsNotActive = true;
                return;
            }
            if (currentShot.x != 0 && currentShot.y != 0)
            {
                shotIsNotActive = false;
            }

            switch (shotDirection[shotDirection.Count - 1])
            {

                case "right":
                    shotIsNotActive = false;
                    if (!DoTask.CheckIfWall(currentShot.y, currentShot.x + 1))
                    {
                        if (Hero.PositionX != currentShot.x)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x += 1;
                    }
                    else
                    {
                        if (Hero.PositionX != currentShot.x)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x = 0;
                        currentShot.y = 0;
                        shotIsNotActive = true;
                    }
                    break;

                case "left":
                    if (!DoTask.CheckIfWall(currentShot.y, currentShot.x - 1))
                    {
                        shotIsNotActive = false;
                        if (Hero.PositionX != currentShot.x)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x -= 1;
                    }
                    else
                    {
                        if (Hero.PositionX != currentShot.x)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x = 0;
                        currentShot.y = 0;

                        shotIsNotActive = true;
                    }
                    break;

                case "up":

                    if (!DoTask.CheckIfWall(currentShot.y - 1, currentShot.x))
                    {
                        shotIsNotActive = false;
                        if (Hero.PositionY != currentShot.y)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.y -= 1;
                    }
                    else
                    {
                        if (Hero.PositionY != currentShot.y)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x = 0;
                        currentShot.y = 0;
                        shotIsNotActive = true;
                    }
                    break;

                case "down":

                    if (!DoTask.CheckIfWall(currentShot.y + 1, currentShot.x))
                    {
                        shotIsNotActive = false;
                        if (Hero.PositionY != currentShot.y)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.y += 1;
                    }
                    else
                    {
                        if (Hero.PositionY != currentShot.y)
                        {
                            DoTask.Print(currentShot.y, currentShot.x, ' ');
                        }
                        currentShot.x = 0;
                        currentShot.y = 0;
                        shotIsNotActive = true;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    public static void DrawShots()
    {

        Shoot();
        if (currentShot.x != 0 && currentShot.y != 0)
        {
            DoTask.Print(currentShot.y, currentShot.x, shotSymbol);
        }
    }



    public static void PlayerMovement()
    {

        DoTask.Print(Rooms.playFieldHeight, 0, new string(' ', 32));

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            if (pressedKey.Key == ConsoleKey.Spacebar)
            {
                if (shotIsNotActive)
                {
                    currentShot.x = PositionX;
                    currentShot.y = PositionY;

                    shotDirection.Add(direction);
                }
            }
            try
            {
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (Rooms.room[PositionY, PositionX - 1] != 2 && Rooms.room[PositionY, PositionX - 1] != 1)
                    {
                        PositionX--;
                        direction = "left";
                        DoTask.Print(PositionY, PositionX + 1, ' ');
                    }
                    else
                    {
                        throw new SteppingOnWallException();
                    }
                }

                if (pressedKey.Key == ConsoleKey.RightArrow)
                {

                    if ((Rooms.room[PositionY, PositionX + 1] != 1) && (Rooms.room[PositionY, PositionX + 1] != 2))
                    {
                        PositionX++;
                        direction = "right";
                        DoTask.Print(PositionY, PositionX - 1, ' ');
                    }
                    else
                    {
                        throw new SteppingOnWallException();
                    }
                }

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {

                    if ((Rooms.room[PositionY - 1, PositionX] != 1) && (Rooms.room[PositionY - 1, PositionX] != 2))
                    {
                        PositionY--;
                        direction = "up";
                        DoTask.Print(PositionY + 1, PositionX, ' ');
                    }
                    else
                    {
                        throw new SteppingOnWallException();
                    }
                }

                if (pressedKey.Key == ConsoleKey.DownArrow)
                {

                    if ((Rooms.room[PositionY + 1, PositionX] != 1) && (Rooms.room[PositionY + 1, PositionX] != 2))
                    {
                        PositionY++;
                        direction = "down";
                        DoTask.Print(PositionY - 1, PositionX, ' ');
                    }
                    else
                    {
                        throw new SteppingOnWallException();
                    }

                }

            }
            catch (SteppingOnWallException)
            {
                string text = "You are poisoned!";
                DoTask.Print(Rooms.playFieldHeight, (Game.gameWidth - InfoPanel.infoPanelWidth - text.Length) / 2, text);
            }
        }
    }


}