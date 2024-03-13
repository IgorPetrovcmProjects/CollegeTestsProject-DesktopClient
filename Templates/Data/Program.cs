namespace Templates.Data;

using Templates.Data.Exception;
using Templates.Data.Routing;

public class Program 
{

    static void Main(string[] args)
    {
        ApplicationBuilder appBuilder;        
        while (true)
        {
            appBuilder = new ApplicationBuilder();
            
            try 
            {
                if (!appBuilder.UseRouting( new TemplateDataRoute( appBuilder ) )) continue;
                if (!appBuilder.UseCommandExecution()) continue;
            }
            catch (ApplicationMiddlewareException ex)
            {
                appBuilder.SendMessageAndExit(ex.Message, 400);
                continue;
            }
            catch (NullReferenceException ex)
            {
                appBuilder.SendMessageAndExit(ex.Message, 523);
                continue;
            }

            appBuilder.SendMessageAndExit("OK", 200);
        }
    }
}