namespace Templates.Data.Routing;

using Templates.Data.Configuration;
using System.Text.RegularExpressions;
using Templates.Data.Exception;

public class UpdateTestRouteHandler : RouteHandler
{
    public UpdateTestRouteHandler(RouteConfiguration configuration) : base (configuration) {}

    public override bool Run(ApplicationBuilder builder)
    {
        Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-]+)");
        
        Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

        string nameTest = match.Groups[1].ToString();

        string sourcePath = builder.GetSourceDirectory() + "/Tests";

        string testPath = sourcePath + nameTest + ".json";

        if (!File.Exists(testPath)){
            builder.AddFinalStatusCode(400);
            throw new ApplicationMiddlewareException("No test with that name was found");
        }

        /*FileInfo sourceFile = new FileInfo(testPath);
        sourceFile.MoveTo(sourceFile.Directory.FullName + "/" + )*/

        using Stream stream = builder.Context.Request.InputStream;
        
        using StreamReader reader = new StreamReader(stream);
         
        string json = reader.ReadToEnd();

        return true;
        
        
    }
}  