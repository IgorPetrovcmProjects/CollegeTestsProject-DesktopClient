namespace Templates.Data.Handler;

using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Templates.Data.Command;
using Templates.Data.Exception;
using Templates.Data.Model;

public class CreateTestHandler : ICommandHandler
{
    private static readonly Regex Recognize = 
        new Regex(@"^(?:(create test) ([a-zA-ZА-Я0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя\s]+)|(add question) ([a-zA-Z0-9А-Я0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя\s]+)|(add answer option) ([a-zA-Z0-9А-Я0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя\s]+)|(add answer) (\d)|(end question)|(end test))");

   private List<Question> questions = new List<Question>();
   
   private string? testTitle;

    private bool _isStartedTest = false;

    private bool _isStartedQuestion = false;

    public bool IsCommandReady {get { return !_isStartedTest; }}

    public ICommand GetCommand()
    {
        return new CreateTestCommand(new Test { title = testTitle, questions = this.questions});
    }

    public void RunRecognize(string line)
    {
        Match match = Recognize.Match(line);

        if (match.Groups[1].ToString() != ""){
            if (_isStartedTest){
                throw new IncorrectCommandSequnce();
            }
            _isStartedTest = true; 

            testTitle = match.Groups[2].ToString();  
            return;
        }
        if (match.Groups[3].ToString() != ""){
            if (_isStartedQuestion || _isStartedTest == false){
                throw new IncorrectCommandSequnce();
            }
            _isStartedQuestion = true;

            questions.Add(new Question { question = match.Groups[4].ToString()});
            return;
        }
        if (match.Groups[5].ToString() != ""){
            if (_isStartedQuestion == false || _isStartedTest == false){
                throw new IncorrectCommandSequnce();
            }
            questions.Last().answerOptions.Add(match.Groups[6].ToString());
            return;
        }
        if (match.Groups[7].ToString() != ""){
            if (_isStartedQuestion == false || _isStartedTest == false){
                throw new IncorrectCommandSequnce();
            }

            if (questions.Last().answers.Count == questions.Last().answerOptions.Count){
                throw new CreateTestHandlerException("The count answers is more than count a answer options");
            }

            questions.Last().answers.Add(int.Parse(match.Groups[8].ToString()));
            return;
        }
        if (match.Groups[9].ToString() != ""){
            if (_isStartedQuestion == false || _isStartedTest == false){
                throw new IncorrectCommandSequnce();
            }

            _isStartedQuestion = false;
            return;
        }
        if (match.Groups[10].ToString() != ""){
            if (_isStartedQuestion || _isStartedTest == false){
                throw new IncorrectCommandSequnce();
            }

            _isStartedTest = false;
            return;
        }

        throw new InputBadFormatException();
    }
}