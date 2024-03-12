namespace Templates.Data.Routing;

using Templates.Data.Configuration;
using Templates.Data.Exception;
using System.Text.RegularExpressions;

public class DeleteTestRouteHandler : RouteHandler
{
    public DeleteTestRouteHandler(RouteConfiguration configuration) : base (configuration) {}

    public override bool Run(ApplicationBuilder builder)
    {
        Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-\s]+)");

        Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

        string nameTest = match.Groups[1].ToString();

        string sourcePath = builder.GetSourceDirectory();

        string sourcePathToFile = sourcePath + "//Tests//" + nameTest + ".json";

        if (!File.Exists(sourcePathToFile)){
            builder.AddFinalStatusCode(400);
            throw new ApplicationMiddlewareException("No test with that name was found");
        }

        File.Delete(sourcePathToFile);

        builder.AddFinalStatusCode(204);

        return false;
    }
}