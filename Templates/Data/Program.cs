namespace Templates.Data;


public class Program 
{
    static void Main(string[] args)
    {
        IEnumerable<string> commands = UserInput();

        foreach (string line in commands)
        {
            
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