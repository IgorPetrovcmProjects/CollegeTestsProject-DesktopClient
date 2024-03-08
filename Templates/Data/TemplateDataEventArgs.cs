namespace Templates.Data;


public class TemplateDataEventArgs : EventArgs
{
    public readonly string sourcePath;

    private event Action<TemplateDataEventArgs>? performed;

    public TemplateDataEventArgs(string sourcePath)
    {
        this.sourcePath = sourcePath;
    }

    public void AddEvent(Action<TemplateDataEventArgs> action)
    {
        performed += action;
    }

    public void RemoveEvent(Action<TemplateDataEventArgs> action)
    {
        performed -= action;
    }

    public void Invoke()
    {
        performed?.Invoke(this);
    }
}