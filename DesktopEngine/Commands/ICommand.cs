namespace DesktopEngine.Command;


public interface ICommand
{
    public void Apply(TemplateDataEventArgs e);
}