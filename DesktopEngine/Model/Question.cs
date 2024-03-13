namespace DesktopEngine.Model;


public class Question
{
    public string? question {get; set;}

    public List<string> answerOptions {get; set;}

    public List<int> answers {get; set;}

    public Question()
    {
        answerOptions = new List<string>();
        answers = new List<int>();
    }
}