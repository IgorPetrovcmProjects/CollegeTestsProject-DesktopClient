namespace Templates.Data.Routing;

using System.Text.RegularExpressions;
using Templates.Data.Configuration;
using Templates.Data.Exception;

public class SourcePathRouteHandler : RouteHandler
{
    public SourcePathRouteHandler( RouteConfiguration configuration ) : base(configuration) {}

    public override bool Run(ApplicationBuilder builder)
    {
        Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-\s]+)");

        Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

        builder.AddFinalStatusCode(204);
        builder.AddFinalStatusCode(400);

        if ( !builder.SetSourceDirectory(match.Groups[1].ToString()) ){
            throw new ApplicationMiddlewareException("The path was not found on the computer");
        }
        
        return false;
    }
}