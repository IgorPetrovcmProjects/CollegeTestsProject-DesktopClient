namespace Templates.Data.Routing;

using System.Net;
using Templates.Data.Configuration;

public interface IRouteHandler
{
    public bool RecognizeRequest(HttpListenerRequest request, RouteConfiguration configuration);

    public bool Run(ApplicationBuilder builder);
}