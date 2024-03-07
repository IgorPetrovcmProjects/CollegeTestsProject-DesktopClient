﻿namespace Templates.Data;

using System.Text;
using Templates.Data.Exception;
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
            try
            {
                producer.RunRecognize(line);

                if (producer.IsCommandReady){

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