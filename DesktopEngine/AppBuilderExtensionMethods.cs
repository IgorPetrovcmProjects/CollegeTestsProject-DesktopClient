namespace DesktopEngine;

using System.Net;
using DesktopEngine.Exception;
using DesktopEngine.Routing;
using DesktopEngine.Handler;

public static partial class ApplicationBuilderExtensionMethods
{
    public static bool IsExit(ApplicationBuilder builder)
    {
        if ( builder.FinalStatusCodes.Any ( x => x == builder.Context.Response.StatusCode ) ){

            builder.SendMessageAndExit( builder.Context.Response.StatusDescription, builder.Context.Response.StatusCode );
            
            return false;
        }

        return true;
    }

    public static bool UseRouting(this ApplicationBuilder builder, IRoute route)
    {
        if (!IsExit(builder)) return false;

        string rawUrl = builder.Context.Request.RawUrl;

        HttpListenerRequest request = builder.Context.Request;

        RouteHandler routeHandler = route.GetRouteHandler(rawUrl);

        if ( routeHandler.RecognizeRequest( request )){
            try 
            {
                if ( !routeHandler.Run(builder) ){
                    if (builder.Context.Response.StatusCode == 0){
                        builder.Context.Response.StatusCode = 204;
                        builder.Context.Response.StatusDescription = "No content";
                    }
                }
            }
            catch (ApplicationMiddlewareException ex)
            {
                builder.Context.Response.StatusCode = 400;
                builder.Context.Response.StatusDescription = ex.Message;
            }
        }
        else 
        {
            throw new ApplicationMiddlewareException("The request is not correct");
        }

        return true;
    }

    public static bool UseCommandExecution(this ApplicationBuilder builder) 
    {
        if (!IsExit(builder)) return false;

        TemplateDataEventArgs templateDataEventArgs = new TemplateDataEventArgs( builder.GetSourceDirectory() );

        CommandProducer producer = new CommandProducer();

        using Stream stream = builder.Context.Request.InputStream;

        using StreamReader read = new StreamReader(stream);

        string commandStrings = read.ReadToEnd();

        commandStrings = commandStrings.Replace("\r\n", "");

        string[] commands = commandStrings.Split(';');
        for (int i = 0; i < commands.Length; i++)
        {
            commands[i] = commands[i].Trim();
        }

        int count = 1;

        foreach (string command in commands)
        {
            try
            {
                if (command == ""){
                    continue;
                }
                if (command == "apply"){
                    templateDataEventArgs.Invoke();

                    continue;
                }

                producer.RunRecognize(command);

                if (producer.IsCommandReady){

                    DesktopEngine.Command.ICommand readyCommand = producer.GetCommand();

                    templateDataEventArgs.AddEvent(readyCommand.Apply);

                    producer = new CommandProducer();
                }

                count++;
            }
            catch (CreateTestHandlerException ex)
            {
                throw new ApplicationMiddlewareException(ex.Message);
            }
            catch (IncorrectCommandSequnce ex)
            {
                throw new ApplicationMiddlewareException(ex.Message);
            }
            catch (InputBadFormatException)
            {
                throw new ApplicationMiddlewareException("line" + count + ": bad format");
            }
        }

        return true;
    }
}