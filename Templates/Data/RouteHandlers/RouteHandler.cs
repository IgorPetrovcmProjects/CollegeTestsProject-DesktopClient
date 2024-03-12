namespace Templates.Data.Routing;

using System.Net;
using Templates.Data.Configuration;

public abstract class RouteHandler
{
    protected RouteConfiguration configuration;

    public RouteHandler(RouteConfiguration routeConfiguration)
    {
        configuration = routeConfiguration;
    }
    

    public bool RecognizeRequest(HttpListenerRequest request)
    {
        if (configuration.query == ""){
            if (request.HttpMethod == configuration.method && request.QueryString.Count == 0)
                return true;
        }
        if (request.HttpMethod == configuration.method && request.QueryString[configuration.query] != null){
            return true;
        }

        return false;
    }

    public abstract bool Run(ApplicationBuilder builder);
}