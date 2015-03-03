using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Artefact
{
    public static int Row { get; set; }
    public static int Col { get; set; }

    public static char[] symbol = { '@', '$' };
    public Artefact(int currentLevel)
    {
        if (currentLevel == 1)
        {
            Row = 3;
            Col = 3;
            //Row = 2;
            //Col = 1;
        }
        else if (currentLevel == 2)
        {
            Row = 3;
            Col = 3;
            //Row = 13;
            //Col = 27;
        }

    }
    public static void DrawArtefact(int currentLevel)
    {
        Artefact artefact = new Artefact(currentLevel);
        DoTask.Print(Row, Col, symbol[currentLevel - 1]);
    }


}


