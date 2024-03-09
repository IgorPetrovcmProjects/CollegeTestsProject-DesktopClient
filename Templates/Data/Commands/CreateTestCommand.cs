namespace Templates.Data.Command;

using System.Text.Json;
using System.Text.Json.Nodes;
using Templates.Data.Model;
using Newtonsoft.Json;
using System.Text;

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

        using (FileStream fs = new FileStream(dirInfo.FullName + "/" + test.title + ".json", FileMode.OpenOrCreate, FileAccess.Write))
        {
            fs.SetLength(0);

            string json = JsonConvert.SerializeObject(test.questions);

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            fs.Write(buffer, 0, buffer.Length);
        }
    }
}