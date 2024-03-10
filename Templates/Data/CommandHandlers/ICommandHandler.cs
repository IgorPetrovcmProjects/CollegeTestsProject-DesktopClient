namespace Templates.Data.Handler;

using System.Text.RegularExpressions;
using Templates.Data.Command;

public interface ICommandHandler
{
    bool IsCommandReady {get;}

    public void RunRecognize(string line);

    public ICommand GetCommand();
}