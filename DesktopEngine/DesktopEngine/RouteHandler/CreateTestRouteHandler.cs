namespace DesktopEngine.Routing;

using DesktopEngine.Configuration;
using DesktopEngine.Exception;
using DesktopEngine.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;

public class CreateTestRouteHandler : RouteHandler
{
	public CreateTestRouteHandler(RouteConfiguration configuration) : base(configuration) { }

	public override bool Run(ApplicationBuilder builder)
	{
		Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-\s]+)");

		Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

		string nameTest = match.Groups[1].ToString();

		string sourcePath = builder.GetSourceDirectory() + "\\Tests\\";

		string pathToTest = sourcePath + nameTest + ".json";

		if (File.Exists(pathToTest))
		{
			builder.AddFinalStatusCode(400);
			throw new ApplicationMiddlewareException("A test with that name already exists");
		}

		using Stream stream = builder.Context.Request.InputStream;

		using StreamReader reader = new StreamReader(stream);

		string sentJson = reader.ReadToEnd();

		stream.Dispose();

		reader.Dispose();

		//List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(sentJson);

		using FileStream fs = new FileStream(pathToTest, FileMode.CreateNew, FileAccess.Write);

		byte[] sentJsonInBytes = Encoding.UTF8.GetBytes(sentJson);

		fs.Write(sentJsonInBytes, 0, sentJsonInBytes.Length);

		return false;
	}
}
