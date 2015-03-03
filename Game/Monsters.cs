using System;
using System.Threading;
// different types of trap dependidng on their speed(if we even implement that)-thrown blade, hole and shot
public enum Monster
{
    Martin,
    Rosen,
    Dimityr
}
//trap facing direction-4 posibilities

public class Monsters
{

    static Thread[] th = new Thread[3];
    public static void MonsterPerLevel(int currentLevel)
    {
        if (currentLevel == 1)
        {
            VisualiseMonster(Monster.Rosen, 4, 7, 4, 7, Direction.StandStill, true, 0);
            Thread.Sleep(45);
            VisualiseMonster(Monster.Martin, 18, 13, 18, 13, Direction.StandStill, true, 1);
            Thread.Sleep(15);
            VisualiseMonster(Monster.Dimityr, 28, 10, 28, 10, Direction.StandStill, true, 2);
        }
        else if (currentLevel == 2)
        {
            VisualiseMonster(Monster.Rosen, 10, 1, 2, 1, Direction.RightLeft, true, 0);
            Thread.Sleep(45);
            VisualiseMonster(Monster.Martin, 6, 2, 12, 2, Direction.LeftRight, true, 1);
            Thread.Sleep(15);

            VisualiseMonster(Monster.Dimityr, 29, 10, 29, 14, Direction.TopDown, true, 2);
            Thread.Sleep(15);

        }
    }

    public static void ResetMonsters()
    {
        for (int i = 0; i < th.Length; i++)
            th[i].Abort();
    }
    //each trap has a type, start position,end position->for knowing when to end and a direction for movement monster will be EXACTLY the same but with different symbols
    public static void VisualiseMonster(Monster type, int startPositionX, int startPositionY, int endPositionX, int endPositionY, Direction direction, bool exists, int threadNumber)
    {
        char symbol = ' ';
        switch (type)
        {
            case Monster.Rosen:
                symbol = 'R';
                break;
            case Monster.Martin:
                symbol = 'M';
                break;
            case Monster.Dimityr:
                symbol = 'D';
                break;
            default:
                break;
        }

        int currentPositionX = startPositionX;
        int currentPositionY = startPositionY;

        th[threadNumber] = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            /* run your code here */
            while (exists)//!(currentPositionX == endPositionX && currentPositionY == endPositionY))
            {
                if (currentPositionX == Hero.currentShot.x && currentPositionY == Hero.currentShot.y)
                {
                    exists = false;
                }
                if (currentPositionX == startPositionX && currentPositionY == startPositionY)
                {
                    switch (direction)
                    {
                        case Direction.TopDown:
                            currentPositionY++;

                            break;
                        case Direction.DownTop:
                            currentPositionY--;

                            break;
                        case Direction.LeftRight:
                            currentPositionX++;

                            break;
                        case Direction.RightLeft:
                            currentPositionX--;

                            break;
                        case Direction.StandStill:

                            break;
                        default:
                            break;

                    }
                }
                DoTask.Print(currentPositionY, currentPositionX, symbol);
                Thread.Sleep(357);
                switch (direction)
                {
                    case Direction.TopDown:
                        currentPositionY++;
                        DoTask.Print(currentPositionY - 1, currentPositionX, ' ');
                        break;
                    case Direction.DownTop:
                        currentPositionY--;
                        DoTask.Print(currentPositionY + 1, currentPositionX, ' ');
                        break;
                    case Direction.LeftRight:
                        currentPositionX++;
                        DoTask.Print(currentPositionY, currentPositionX - 1, ' ');
                        break;
                    case Direction.RightLeft:
                        currentPositionX--;
                        DoTask.Print(currentPositionY, currentPositionX + 1, ' ');
                        break;
                    case Direction.StandStill:

                        break;
                    default:
                        break;
                }
                if ((currentPositionX == endPositionX && currentPositionY == endPositionY) || (currentPositionX == startPositionX && currentPositionY == startPositionY))
                {
                    switch (direction)
                    {
                        case Direction.TopDown:
                            currentPositionY--;
                            direction = Direction.DownTop;
                            break;
                        case Direction.DownTop:
                            currentPositionY++;
                            direction = Direction.TopDown;
                            break;
                        case Direction.LeftRight:
                            currentPositionX--;
                            direction = Direction.RightLeft;
                            break;
                        case Direction.RightLeft:
                            currentPositionX++;
                            direction = Direction.LeftRight;
                            break;
                        case Direction.StandStill:

                            break;
                        default:
                            break;
                    }


                }

                if (Hero.PositionX == currentPositionX && Hero.PositionY == currentPositionY)
                {
                    Hero.lives--;
                }

            }
        });
        th[threadNumber].Start();
    }
}