using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class Question
{
    public const int AnswersCount = 4;

    private string[] wrongAnswers;
    private string correctAnswer;

    private static Random rand = new Random();

    public Question(XElement questionXml)
    {
        this.Level = int.Parse(questionXml.Attribute("level").Value);
        this.Text = questionXml.Elements("text")
                               .First()
                               .Value
                               .Trim();
        this.correctAnswer = questionXml.Elements("answers")
                                        .First()
                                        .Elements("answer")
                                        .First(answer => answer.Attribute("isCorrect").Value == "true")
                                        .Value
                                        .Trim();
        this.wrongAnswers = questionXml.Elements("answers")
                                       .First()
                                       .Elements("answer")
                                       .Where(answer => answer.Attribute("isCorrect").Value == "false")
                                       .Select(answer => answer.Value.Trim())
                                       .ToArray();
    }

    public string[] Answers
    {
        get
        {
            HashSet<string> selectedWrongAnswers = new HashSet<string>();

            while (selectedWrongAnswers.Count < AnswersCount - 1)
            {
                int index = rand.Next(this.wrongAnswers.Length);
                selectedWrongAnswers.Add(this.wrongAnswers[index]);
            }
            //selectedWrongAnswer has a count of AnswersCount - 1

            List<string> answers = new List<string>(selectedWrongAnswers);

            answers.Add(this.correctAnswer);

            //shuffle the selected wrong and correct answers
            for (int i = 0; i < answers.Count - 1; i++)
            {
                int index = rand.Next(i + 1, answers.Count);
                var temp = answers[i];
                answers[i] = answers[index];
                answers[index] = temp;
            }

            return answers.ToArray();
        }
    }

    public string Text { get; set; }

    public int Level { get; set; }

    public bool IsCorrectAnswer(int index)
    {
        return this.correctAnswer == this.selectedAnswers[index];
    }

    string[] selectedAnswers;

    internal void Draw()
    {
        int width = Game.gameWidth - InfoPanel.infoPanelWidth - 5;
        //DoTask.Print(Rooms.playFieldHeight, (Game.gameWidth - InfoPanel.infoPanelWidth - question.Length) / 2, question);
        DoTask.Print(Rooms.playFieldHeight, 1, this.Text, width);
        selectedAnswers = this.Answers;
        for (int i = 0; i < selectedAnswers.Length; i++)
        {
            string answer = (i + 1) + " " + selectedAnswers[i];
            DoTask.Print(Rooms.playFieldHeight + (this.Text.Length / width) + i + 1, 1, answer, width);
        }
    }

    internal void Clear()
    {
        int width = Game.gameWidth - InfoPanel.infoPanelWidth - 5;
        int rowsToClear = this.Text.Length / width + 1;
        rowsToClear += selectedAnswers.Select(answer => answer.Length / width + 1).Sum();
        int charsToClear = rowsToClear * width;
        DoTask.Print(Rooms.playFieldHeight, 1, new string(' ', charsToClear), width);

        //DoTask.Print(Rooms.playFieldHeight, 1, new string(' ', this.Text.Length), width);
        //for (int i = 0; i < selectedAnswers.Length; i++)
        //{
        //    string answer = (i + 1) + " " + selectedAnswers[i];
        //    string answerToClear = new string(' ', answer.Length + 2); 
        //    DoTask.Print(Rooms.playFieldHeight + (this.Text.Length / width) + i + 1, 1, answerToClear, width);
        //}
    }
}