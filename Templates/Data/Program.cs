namespace Templates.Data;

using System.Text;
using System.Windows.Input;
using Templates.Data.Exception;
using Templates.Data.Handler;
using Templates.Data.Command;

public class Program 
{
    static TemplateDataEventArgs? templateDataEventArgs;

    static void Main(string[] args)
    {
        if (args.Length == 0){

            System.Console.WriteLine("You have not entered the path to the source folder");
            
            Environment.Exit(0);
        }

        templateDataEventArgs = new TemplateDataEventArgs(args[0]);

        CommandProducer producer = new CommandProducer();

        IEnumerable<string> commands = UserInput();

        foreach (string line in commands)
        {
            try
            {
                if (line == "apply"){
                    templateDataEventArgs.Invoke();

                    continue;
                }

                producer.RunRecognize(line);

                if (producer.IsCommandReady){

                    Templates.Data.Command.ICommand command = producer.GetCommand();

                    templateDataEventArgs.AddEvent(command.Apply);

                    producer = new CommandProducer();
                }
            }
            catch (CreateTestHandlerException ex)
            {
                System.Console.WriteLine(ex);
            }
            catch (IncorrectCommandSequnce ex)
            {
                System.Console.WriteLine(ex);
            }
            catch (InputBadFormatException)
            {
                System.Console.WriteLine("bad format");
            }
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