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
		Regex recognizeRegex = new Regex(@$"[a-zA-Z0-9\?\/\\=\:\.]+\?{configuration.query}=([a-zA-Z0-9\?\/\\=\:\.\-\s]+)");

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

		Test test = JsonConvert.DeserializeObject<Test>(sentJson);

		FileInfo fileWithTest = new FileInfo(pathToTest);

		string pathToUpdateTest = fileWithTest.Directory.FullName + "\\" + test.title + ".json";
		fileWithTest.MoveTo(pathToUpdateTest);

		using FileStream fs = new FileStream(pathToUpdateTest, FileMode.Open, FileAccess.ReadWrite);

		byte[] currentJsonInBytes = new byte[fs.Length];
		fs.Read(currentJsonInBytes, 0, currentJsonInBytes.Length);

		string currentJson = Encoding.UTF8.GetString(currentJsonInBytes);

		List<Question> currentQuestions = JsonConvert.DeserializeObject<List<Question>>(currentJson);

		currentQuestions = test.questions;

		string newJson = JsonConvert.SerializeObject(currentQuestions);

		byte[] newJsonInBytes = Encoding.UTF8.GetBytes(newJson);

		fs.SetLength(0);

		fs.Write(newJsonInBytes, 0, newJsonInBytes.Length);

		builder.AddFinalStatusCode(204);

		return false;
	}
}