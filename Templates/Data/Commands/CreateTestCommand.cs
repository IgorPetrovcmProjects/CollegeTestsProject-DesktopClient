namespace Templates.Data.Command;

using System.Text.Json;
using System.Text.Json.Nodes;
using Templates.Data.Model;

public class CreateTestCommand : ICommand
{
    private readonly Test test;

    public CreateTestCommand(Test test)
    {
        this.test = test;
    }

    public void Apply(TemplateDataEventArgs e)
    {
        DirectoryInfo dirInfo = Directory.CreateDirectory(e.sourcePath + "/Tests");

        using (FileStream fs = new FileStream(dirInfo.FullName + "/" + test.title + ".json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            fs.SetLength(0);

            JsonSerializer.Serialize <List<Question>> (fs, test.questions);
        }
    }
}