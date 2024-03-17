namespace DesktopEngine.Routing;

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using DesktopEngine.Configuration;
using DesktopEngine.Exception;

public class SourcePathRouteHandler : RouteHandler
{
	public SourcePathRouteHandler(RouteConfiguration configuration) : base(configuration) { }

	public override bool Run(ApplicationBuilder builder)
	{
		string url = builder.Context.Request.Url.ToString();

		url = url.Replace("%5C", @"\");

		Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяa-zA-Z0-9\?\/\\=\:\.\-\s]+)");

		Match match = recognizeRegex.Match(url);

		builder.AddFinalStatusCode(204);
		builder.AddFinalStatusCode(400);

		if (!builder.SetSourceDirectory(match.Groups[1].ToString()))
		{
			throw new ApplicationMiddlewareException("The path was not found on the computer");
		}

		return false;
	}
}