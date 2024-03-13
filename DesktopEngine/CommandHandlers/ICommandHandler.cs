namespace DesktopEngine.Handler;

using DesktopEngine.Command;

public interface ICommandHandler
{
    bool IsCommandReady {get;}

    public void RunRecognize(string line);

    public ICommand GetCommand();
}