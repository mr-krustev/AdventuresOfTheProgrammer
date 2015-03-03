using System;
using System.Threading;
// different types of trap dependidng on their speed(if we even implement that)-thrown blade, hole and shot
public enum TrapType
{
    Blade,
    Hole,
    Stone
}
//trap facing direction-4 posibilities
public enum Direction
{
    TopDown,
    DownTop,
    LeftRight,
    RightLeft,
    StandStill
}



public class Traps
{
    static Thread[] th=new Thread[3];
    public static void TrapsPerLevel(int currentLevel)
    {
        if (currentLevel == 1)
        {
            VisualiseTrap(TrapType.Blade, 5, 6, 10, 6, Direction.LeftRight,0);
            Thread.Sleep(45);
            VisualiseTrap(TrapType.Blade, 9, 3, 4, 3, Direction.RightLeft,1);
            Thread.Sleep(15);
            VisualiseTrap(TrapType.Stone, 18, 6, 25, 6, Direction.LeftRight,2);
            
            
        }
        else if (currentLevel == 2)
        {

            VisualiseTrap(TrapType.Blade, 15, 13, 15, 8, Direction.DownTop,0);
            Thread.Sleep(45);
            VisualiseTrap(TrapType.Stone, 22, 9, 22, 14, Direction.TopDown,1);
            Thread.Sleep(15);
            VisualiseTrap(TrapType.Stone, 7, 8, 3, 8, Direction.RightLeft,2);
            Thread.Sleep(15);
            

        }
    }
    public static void ResetTraps()
    {
        for (int i = 0; i < th.Length;i++ )
            th[i].Abort();
    }

    //each trap has a type, start position,end position->for knowing when to end and a direction for movement monster will be EXACTLY the same but with different symbols
    public static void VisualiseTrap(TrapType type, int startPositionX, int startPositionY, int endPositionX, int endPositionY, Direction direction,int threadNumber)
    {
        char symbol = ' ';
        switch (type)
        {
            case TrapType.Blade:
                symbol = '+';
                break;
            case TrapType.Hole:
                symbol = 'o';
                break;
            case TrapType.Stone:
                symbol = '.';
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
            while (true)//!(currentPositionX == endPositionX && currentPositionY == endPositionY))
            {
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

                    default:
                        break;
                }
                if (currentPositionX == endPositionX && currentPositionY == endPositionY)
                {
                    currentPositionX = startPositionX;
                    currentPositionY = startPositionY;

                }

                if(Hero.PositionX == currentPositionX && Hero.PositionY == currentPositionY)
                {
                    Hero.lives--;
                }
            }

        });
        th[threadNumber].Start();
       
    }
}