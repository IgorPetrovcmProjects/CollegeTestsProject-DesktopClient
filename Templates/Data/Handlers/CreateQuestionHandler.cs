namespace Templates.Data.Handler;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Templates.Data.Command;

public class CreateQuestionHandler : ICommandHandler
{
    private static readonly Regex Recognize = new Regex(@"^(?:(create test) ([a-zA-Z0-9]+)|(add question) ([a-zA-Z0-9]+)|(add answer option) ([a-zA-Z0-9]+)|(add answer) (\d)|(end question)|(end test))");

    private bool _isStartedTest;

    private bool _isStartedQuestion;

    public bool IsCommandReady {get { return !_isStartedTest; }}

    public ICommand GetCommand()
    {
        throw new NotImplementedException();
    }

    public void RunRecognize(string line)
    {
        Match match = Recognize.Match(line);

        
    }
}