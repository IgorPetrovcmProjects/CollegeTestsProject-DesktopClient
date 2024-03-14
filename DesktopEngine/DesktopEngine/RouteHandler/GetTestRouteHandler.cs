namespace DesktopEngine.Routing;

using System.Text;
using System.Text.RegularExpressions;
using DesktopEngine.Configuration;
using DesktopEngine.Exception;

public class GetTestRouteHandler : RouteHandler
{
	public GetTestRouteHandler(RouteConfiguration configuration) : base(configuration) { }

	public override bool Run(ApplicationBuilder builder)
	{
		Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяa-zA-Z0-9\?\/\\=\:\.\-\s]+)");

		Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

		string nameTest = match.Groups[1].ToString();

		string pathToTest = builder.GetSourceDirectory() + "\\Tests\\" + nameTest + ".json";

		if (!File.Exists(pathToTest))
		{
			builder.AddFinalStatusCode(400);
			throw new ApplicationMiddlewareException("No test with that name was found");
		}

		using FileStream fs = new FileStream(pathToTest, FileMode.Open, FileAccess.Read);

		byte[] jsonInBytes = new byte[fs.Length];

		fs.Read(jsonInBytes, 0, jsonInBytes.Length);

		string jsonWithTest = Encoding.UTF8.GetString(jsonInBytes);

		builder.Context.Response.StatusCode = 200;
		builder.ServerAnswer = jsonWithTest;
		builder.Context.Response.ContentType = "application/json";

		builder.AddFinalStatusCode(200);

		return false;
	}
}