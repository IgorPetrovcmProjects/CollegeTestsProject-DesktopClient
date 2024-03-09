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

        IEnumerable<string> commands = args.Length == 2 
            ? FileInputs(args[1])
            : UserInputs();

        //IEnumerable<string> commands = FileInputs(@"C:\Users\igorp\MyProjects\CollegeTestsProject\CollegeTestsProject-DesktopClient\Templates\Data.Test\set_commands.txt");

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

    private static IEnumerable<string> UserInputs()
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

    private static IEnumerable<string> FileInputs(string path)
    {
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i++)
        {
            yield return lines[i];
        }
    }
}