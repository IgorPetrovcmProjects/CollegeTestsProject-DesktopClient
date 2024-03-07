namespace Templates.Data.Model;


public class Question
{
    public string? question;

    public List<string> answerOptions = new List<string>();

    public List<int> answers = new List<int>();
}