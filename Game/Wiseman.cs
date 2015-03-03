using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Wiseman
{
    public static Question[] questions;
    public static int Row { get; set; }
    public static int Col { get; set; }

    public static char[] symbol = { '%', '#' };

    public Wiseman(int currentLevel)
    {
       
        if (currentLevel == 1)
        {
            Row = 4;
            Col = 3;

            //Row = 3;
            //Col = 1;
        }
        else if (currentLevel == 2)
        {
            Row = 2;
            Col = 16;
        }
    }

    

    public static void DrawWiseman(int currentLevel)
    {
        Wiseman artefact = new Wiseman(currentLevel);
        DoTask.Print(Row, Col, symbol[currentLevel - 1]);
    }
    static Random rand = new Random();

    public static Question GetQuestion(Question[] questions, int lvl = 1)
    {
        //check if lvl in bounds
        var questionsWithLevel = questions.Where(question => question.Level == lvl);

        //check if any question with the given level

        int index = rand.Next(questionsWithLevel.Count());
        return questionsWithLevel.ElementAt(index);
    }
}
