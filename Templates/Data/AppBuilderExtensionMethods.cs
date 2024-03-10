namespace Templates.Data;

using System.Net;
using Templates.Data.Routing;

public static class ApplicationBuilderExtensionMethods
{
    public static void UseRouting(this ApplicationBuilder builder, IRoute route)
    {
        string rawUrl = builder.Context.Request.RawUrl;

        HttpListenerRequest request = builder.Context.Request;

        IRouteHandler routeHandler = route.GetRouteHandler(rawUrl);

        if ( routeHandler.RecognizeRequest( request, builder.GetRouteConfiguration(rawUrl) ) )
    }
}