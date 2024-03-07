namespace Templates.Data.Command;

using Templates.Data.Model;

public class CreateTestCommand : ICommand
{
    private readonly Test test;

    public CreateTestCommand(Test test)
    {
        this.test = test;
    }

    public void Apply()
    {
        throw new NotImplementedException();
    }
}