using System.Text.RegularExpressions;
using Templates.Data.Command;
using Templates.Data.Exception;

namespace Templates.Data.Handler;


public class CommandProducer : ICommandHandler
{
    private Dictionary<Regex, Func<ICommandHandler>> _commands = new Dictionary<Regex, Func<ICommandHandler>>() 
    {
        { new Regex("^create test"), () => new CreateTestHandler()}
    };

    private ICommandHandler? _currentHandler;

    public bool IsCommandReady { get {
        if (_currentHandler == null)
            return false;
        
        return _currentHandler.IsCommandReady;
    }}

    public Templates.Data.Command.ICommand GetCommand()
    {
        if (_currentHandler != null){
            return _currentHandler.GetCommand();
        }

        throw new InputBadFormatException();
    }

    public void RunRecognize(string line)
    {
        if (_currentHandler == null)
        {
            foreach (KeyValuePair<Regex, Func<ICommandHandler>> pair in _commands)
            {
                if (pair.Key.IsMatch(line))
                {
                    _currentHandler = pair.Value.Invoke();
                    break;
                }
            }

            if (_currentHandler == null){
                throw new InputBadFormatException();
            }
        }

        _currentHandler.RunRecognize(line);
    }
}