namespace Templates.Data;

using System.Net;
using System.Text.RegularExpressions;
using Templates.Data.Exception;
using Templates.Data.Routing;

public static class ApplicationBuilderExtensionMethods
{
    public static bool UseRouting(this ApplicationBuilder builder, IRoute route)
    {
        if ( builder.FinalStatusCodes.Any(x => x == builder.Context.Response.StatusCode) ){
            builder.Listener.Close();
            return false;
        }

        string rawUrl = builder.Context.Request.RawUrl;

        HttpListenerRequest request = builder.Context.Request;

        Regex recognizeRaw = new Regex(@"(\/[a-zA-Z0-9]+\/)[a-zA-Z0-9\?\/\\=\:\.\-]+");

        IRouteHandler routeHandler = route.GetRouteHandler(rawUrl);

        Match match = recognizeRaw.Match(rawUrl);

        if ( routeHandler.RecognizeRequest( request, builder.GetRouteConfiguration(match.Groups[1].ToString()) ) ){
            try 
            {
                if ( !routeHandler.Run(builder) ){
                    builder.Context.Response.StatusCode = 204;
                }
            }
            catch (ApplicationRoutesException ex)
            {
                builder.Context.Response.StatusCode = 400;
                builder.Context.Response.StatusDescription = ex.Message;
            }
        }

        return true;
    }
}