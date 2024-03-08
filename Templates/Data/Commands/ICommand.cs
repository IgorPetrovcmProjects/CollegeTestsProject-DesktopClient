namespace Templates.Data.Command;


public interface ICommand
{
    public void Apply(TemplateDataEventArgs e);
}