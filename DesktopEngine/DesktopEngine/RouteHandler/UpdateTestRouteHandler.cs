namespace DesktopEngine.Routing;

using DesktopEngine.Configuration;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using DesktopEngine.Exception;
using DesktopEngine.Model;
using System.Text;

public class UpdateTestRouteHandler : RouteHandler
{
	public UpdateTestRouteHandler(RouteConfiguration configuration) : base(configuration) { }

	public override bool Run(ApplicationBuilder builder)
	{
		Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяa-zA-Z0-9\?\/\\=\:\.\-\s]+)");

		Match match = recognizeRegex.Match(builder.Context.Request.Url.ToString());

		string nameTest = match.Groups[1].ToString();

		string sourcePath = builder.GetSourceDirectory() + "\\Tests\\";

		string pathToTest = sourcePath + nameTest + ".json";

		if (!File.Exists(pathToTest))
		{
			builder.AddFinalStatusCode(400);
			throw new ApplicationMiddlewareException("No test with that name was found");
		}

		using Stream stream = builder.Context.Request.InputStream;

		using StreamReader reader = new StreamReader(stream);

		string sentJson = reader.ReadToEnd();

		stream.Dispose();

		reader.Dispose();

		Test sentTest = JsonConvert.DeserializeObject<Test>(sentJson);

		FileInfo fileWithTest = new FileInfo(pathToTest);

		string pathToUpdateTest = fileWithTest.Directory.FullName + "\\" + sentTest.title + ".json";
		fileWithTest.MoveTo(pathToUpdateTest);

		using FileStream fs = new FileStream(pathToUpdateTest, FileMode.Open, FileAccess.Write);

		string newJson = JsonConvert.SerializeObject(sentTest.questions);

		byte[] newJsonInBytes = Encoding.UTF8.GetBytes(newJson);

		fs.SetLength(0);

		fs.Write(newJsonInBytes, 0, newJsonInBytes.Length);

		builder.Context.Response.StatusCode = 200;
		builder.AddFinalStatusCode(200);

		return false;
	}
}