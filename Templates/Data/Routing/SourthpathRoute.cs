namespace Templates.Data.Routing;

using System.Net;
using System.Text.RegularExpressions;
using Templates.Data.Configuration;
using Templates.Data.Exception;

public class SourthpathRoute : IRouteHandler
{
    private RouteConfiguration configuration;

    public bool RecognizeRequest(HttpListenerRequest request, RouteConfiguration configuration)
    {
        if (request.HttpMethod == configuration.method && request.QueryString[configuration.query] != null){

            this.configuration = configuration;

            return true;
        }

        return false;
    }

    public bool Run(ApplicationBuilder builder)
    {
        Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-]+)");

        Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

        builder.AddFinalStatusCode(204);
        builder.AddFinalStatusCode(400);

        if ( !builder.SetSourceDirectory(match.Groups[1].ToString()) ){
            throw new ApplicationRoutesException("The path was not found on the computer");
        }
        
        return false;
    }
}