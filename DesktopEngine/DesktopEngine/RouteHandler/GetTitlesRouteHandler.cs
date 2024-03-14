namespace DesktopEngine.Routing;

using DesktopEngine.Configuration;
using DesktopEngine.Exception;
using Newtonsoft.Json;
using DesktopEngine;


public class GetTitlesRouteHandler : RouteHandler
{
	public GetTitlesRouteHandler(RouteConfiguration configuration) : base(configuration) { }

	public override bool Run(ApplicationBuilder builder)
	{
		string sourcePath = builder.GetSourceDirectory();

		string allTestsPath = sourcePath + "//Tests//";

		if (!Directory.Exists(allTestsPath))
		{
			builder.AddFinalStatusCode(400);
			throw new ApplicationMiddlewareException("No tests found");
		}

		FileInfo[] tests = new DirectoryInfo(allTestsPath).GetFiles();

		List<string> titles = new List<string>();

		foreach (FileInfo test in tests)
		{
			titles.Add(test.Name.Replace(".json", ""));
		}

		string jsonWithTitles = JsonConvert.SerializeObject(titles);

		builder.Context.Response.StatusCode = 200;

		builder.Context.Response.StatusDescription = jsonWithTitles;

		builder.AddFinalStatusCode(200);

		return false;
	}
}