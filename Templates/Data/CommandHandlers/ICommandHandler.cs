namespace Templates.Data.Handler;

using Templates.Data.Command;

public interface ICommandHandler
{
    bool IsCommandReady {get;}

    public void RunRecognize(string line);

    public ICommand GetCommand();
}