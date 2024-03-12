namespace Templates.Data.Handler;

using Templates.Data.Command;
using System.Text.RegularExpressions;

public class UpdateTestHandler : ICommandHandler
{
    private static readonly Regex RecognizeRegex = 
        new Regex(@"^(?:update test) ([a-zA-ZА-Я0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя\s]+)|(update question) ([a-zA-ZА-Я0-9АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя\s]+)");

    public bool IsCommandReady => throw new NotImplementedException();

    public ICommand GetCommand()
    {
        throw new NotImplementedException();
    }

    public void RunRecognize(string line)
    {
        throw new NotImplementedException();
    }
}