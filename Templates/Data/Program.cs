namespace Templates.Data;

using System.Text;
using Templates.Data.Handler;

public class Program 
{
    static void Main(string[] args)
    {
        CommandProducer producer = new CommandProducer();

        Console.InputEncoding = Encoding.UTF8;

        IEnumerable<string> commands = UserInput();

        foreach (string line in commands)
        {
            byte[] lineInBytes = Encoding.UTF8.GetBytes(line);
            string readyLine = Encoding.UTF8.GetString(lineInBytes);
            producer.RunRecognize(readyLine);
        }
    }

    private static IEnumerable<string> UserInput()
    {
        while (true)
        {
            System.Console.WriteLine("Please, enter a command or press Enter to exit");
            System.Console.Write(">");

            string? input = Console.ReadLine();

            if (input == null || input.Trim() == ""){
                yield break;
            }

            yield return input;
        }
    }


}