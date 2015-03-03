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
    public static int currentLevel = 1;
    public static bool newLevel = false;
    public static bool getArtefact = false;

    public static int score = 500;

    public static int reprintWalls = 0;
    public static Question[] questions;


    public static string heroName = "";

    static bool isGameRunning;
    static bool isQuestionPrinted;

    static Question selectedQuestion;

    static Question[] LoadQuestions()
    {
        string path = "..\\..\\questions\\questions.xml";
        XDocument doc = XDocument.Load(path);
        var questionsRoot = doc.Root;
        Question[] questions = questionsRoot.Elements("question")
                                            .Select(questionXml => new Question(questionXml)).ToArray();

        return questions;
    }

    public static List<string> topFiveScores = new List<string>();

    static void SortList(List<string> topFiveScores)
    {
        for (int i = 0; i < topFiveScores.Count; i++)
        {
            for (int j = i + 1; j < topFiveScores.Count; j++)
            {
                if (int.Parse(topFiveScores[i].Substring(4)) < int.Parse(topFiveScores[j].Substring(4)))
                {
                    string tempString = topFiveScores[i];
                    topFiveScores[i] = topFiveScores[j];
                    topFiveScores[j] = tempString;
                }
            }
        }
    }

    static void Main()
    {

        using (StreamReader read = new StreamReader(@"..//..//HighScores.txt"))
        {
            while (!read.EndOfStream)
            {
                topFiveScores.Add(read.ReadLine());
            }
        }

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
        InfoPanel.PrintRoomName(Game.currentLevel);

        InfoPanel.CurrentStats();
        Hero.GetInitialPosition(currentLevel);

        Artefact.DrawArtefact(currentLevel);
        Traps.TrapsPerLevel(currentLevel);
        Monsters.MonsterPerLevel(currentLevel);
        questions = LoadQuestions();
        isGameRunning = true;
        isQuestionPrinted = false;
        try
        {
            #region Game

            while (true)
            {
                if (isGameRunning)
                {
                    
                    
                    Hero.PlayerMovement();
                    Hero.DrawPlayer(Hero.PositionX, Hero.PositionY);

                    if (Hero.lives == 0)
                    {
                        // Add end-game stuff;
                        throw new EndOfGameException(false);
                    }


                    if (!getArtefact)
                    {
                        Artefact.DrawArtefact(currentLevel);
                    }
                    if (Hero.PositionX == Artefact.Col && Hero.PositionY == Artefact.Row && !getArtefact)
                    {
                        getArtefact = true;
                        score += 100;
                        if (currentLevel == 2)
                        {
                            Hero.PositionX = 13;                            
                        }
                    }
                    if (getArtefact)
                    {
                        Wiseman.DrawWiseman(currentLevel);
                        if (Hero.PositionX == Wiseman.Col && Hero.PositionY == Wiseman.Row)
                        {
                            Traps.ResetTraps();
                            selectedQuestion = Wiseman.GetQuestion(questions, currentLevel);
                            isGameRunning = false;
                        }
                    }

                    Hero.DrawShots();

                    if (newLevel)
                    {
                        score += 200;
                        
                        Monsters.ResetMonsters();
                        getArtefact = false;
                        newLevel = false;
                        Console.Clear();
                        Rooms.FillRoomsWithSymbols(Rooms.room, currentLevel);
                        DoTask.PrintArray(InfoPanel.infoPanel, InfoPanel.infoPanelHeight, InfoPanel.infoPanelWidth,
                            Rooms.playFieldWidth);
                        DoTask.PrintArray(Rooms.room, Rooms.playFieldHeight, Rooms.playFieldWidth);
                        InfoPanel.PrintRoomName(currentLevel);
                        InfoPanel.CurrentStats();
                        Hero.GetInitialPosition(currentLevel);
                        Artefact.DrawArtefact(currentLevel);
                        Traps.TrapsPerLevel(currentLevel);
                        Monsters.MonsterPerLevel(currentLevel);
                    }

                    Thread.Sleep(375);
                    InfoPanel.CurrentScore();
                    score--;

                    reprintWalls++;

                    if (reprintWalls % 8 == 0)
                    {
                        InfoPanel.Boarders();
                        DoTask.PrintArray(InfoPanel.infoPanel, InfoPanel.infoPanelHeight, InfoPanel.infoPanelWidth,
                            Rooms.playFieldWidth);
                        DoTask.PrintArray(Rooms.room, Rooms.playFieldHeight, Rooms.playFieldWidth);
                        InfoPanel.CurrentStats();
                        InfoPanel.PrintRoomName(currentLevel);
                        reprintWalls = 0;
                    }
                }
                else
                {
                    if (!isQuestionPrinted)
                    {
                        selectedQuestion.Draw();
                        isQuestionPrinted = true;
                    }
                    else
                    {
                        if (Console.KeyAvailable)
                        {
                            var info = Console.ReadKey();
                            int selectedAnswer = 0;
                            switch (info.Key)
                            {
                                case ConsoleKey.D1:
                                case ConsoleKey.D2:
                                case ConsoleKey.D3:
                                case ConsoleKey.D4:
                                    selectedAnswer = info.KeyChar - '0';
                                    break;
                            }
                            if (1 <= selectedAnswer && selectedAnswer <= 4)
                            {
                                bool isCorrect = selectedQuestion.IsCorrectAnswer(selectedAnswer - 1);
                                selectedQuestion.Clear();
                                if (isCorrect)
                                {
                                    newLevel = true;
                                    currentLevel++;
                                    if (currentLevel == 3)
                                    {
                                        throw new EndOfGameException(true);
                                    }
                                    isGameRunning = true;
                                    selectedQuestion = null;
                                }
                                else
                                {
                                    selectedQuestion = Wiseman.GetQuestion(questions, currentLevel);
                                }
                                isQuestionPrinted = false;
                            }

                        }
                    }
                }
            }

            #endregion
        }
        catch (EndOfGameException ex)
        {
            topFiveScores.Add(string.Join("-", heroName, score));

            SortList(topFiveScores);

            StreamWriter writeScore = new StreamWriter("HighScores.txt");
            using (writeScore)
            {
                for (int i = 0; i < topFiveScores.Count && i < 5; i++)
                {
                    writeScore.WriteLine(topFiveScores[i]);
                }
            }
            Console.Clear();

            Console.WriteLine(ex.GameWon ? "You won!" : "Game over");
            foreach (string userScore in topFiveScores)
            {
                Console.WriteLine(userScore);
            }
        }

    }
}