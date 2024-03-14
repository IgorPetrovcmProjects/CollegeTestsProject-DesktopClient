namespace DesktopEngine;

using System.Text;
using System.Net;
using Newtonsoft.Json;
using DesktopEngine.Configuration;
using DesktopEngine.Exception;

public class ApplicationBuilder
{
	private Dictionary<string, RouteConfiguration> configurations = new Dictionary<string, RouteConfiguration>();

	private HashSet<int> finalStatusCodes = new HashSet<int>();
	public HashSet<int> FinalStatusCodes { get { return finalStatusCodes; } }

	private const string PathToRouteConfigurationFile = "Configuration/route-configuration.json";

	private const string PathToAppConfigurationFile = "Configuration/configuration.json";

	private const string Url = "http://127.0.0.1:3330";

	private HttpListener listener = new HttpListener();
	public HttpListener Listener { get { return listener; } }

	private HttpListenerContext _context;

	public HttpListenerContext Context { get { return _context; } }

	private void AssignConfigurationForUrls()
	{
		using FileStream fs = new FileStream(PathToRouteConfigurationFile, FileMode.Open, FileAccess.ReadWrite);

		byte[] jsonInBytes = new byte[fs.Length];

		fs.Read(jsonInBytes, 0, jsonInBytes.Length);

		string json = Encoding.UTF8.GetString(jsonInBytes);

		configurations = JsonConvert.DeserializeObject<Dictionary<string, RouteConfiguration>>(json);
	}

	private void AssignRoutes()
	{
		if (configurations.Count == 0)
		{
			throw new ApplicationMiddlewareException("The Routes was not found");
		}
		foreach (string key in configurations.Keys)
		{
			listener.Prefixes.Add(Url + key);
		}
	}

	public ApplicationBuilder()
	{
		AssignConfigurationForUrls();
		AssignRoutes();

		listener.Start();

		_context = listener.GetContext();
	}

	public RouteConfiguration GetRouteConfiguration(string route)
	{
		return configurations[route];
	}

	public void AddFinalStatusCode(int statusCode)
	{
		finalStatusCodes.Add(statusCode);
	}

	public void SendMessageAndExit(string message, int status)
	{
		byte[] messageInBytes = Encoding.UTF8.GetBytes(message);

		_context.Response.ContentLength64 = messageInBytes.Length;

		_context.Response.StatusCode = status;

		_context.Response.OutputStream.Write(messageInBytes);

		listener.Stop();
		listener.Close();
	}

	public bool SetSourceDirectory(string path)
	{
		if (Directory.Exists(path))
		{
			ApplicationConfiguration appConfiguration = new ApplicationConfiguration()
			{
				sourcePath = path
			};

			using FileStream fs = new FileStream(PathToAppConfigurationFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);

			byte[] jsonInBytes = new byte[fs.Length];

			fs.Read(jsonInBytes, 0, jsonInBytes.Length);

			string json = Encoding.UTF8.GetString(jsonInBytes);

			ApplicationConfiguration currentAppConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(json);

			currentAppConfiguration = appConfiguration;

			fs.SetLength(0);

			json = JsonConvert.SerializeObject(currentAppConfiguration);

			jsonInBytes = Encoding.UTF8.GetBytes(json);

			fs.Write(jsonInBytes, 0, jsonInBytes.Length);

			return true;
		}

		return false;
	}

	public string GetSourceDirectory()
	{
		using FileStream fs = new FileStream(PathToAppConfigurationFile, FileMode.Open, FileAccess.Read);

		byte[] jsonInBytes = new byte[fs.Length];

		fs.Read(jsonInBytes, 0, jsonInBytes.Length);

		string json = Encoding.UTF8.GetString(jsonInBytes);

		ApplicationConfiguration? appConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(json);

		if (appConfiguration == null || appConfiguration.sourcePath?.Trim() == ""){
			if (!Directory.Exists(Environment.CurrentDirectory + "\\source")){
				Directory.CreateDirectory(Environment.CurrentDirectory + "\\source");
			}

			DirectoryInfo newSourceDirectory = new DirectoryInfo(Environment.CurrentDirectory + "\\source");

			appConfiguration = new ApplicationConfiguration();

			appConfiguration.sourcePath = newSourceDirectory.FullName;

			string newJson = JsonConvert.SerializeObject(appConfiguration);

			byte[] newJsonInBytes = Encoding.UTF8.GetBytes(newJson);

			fs.SetLength(0);

			fs.Write(newJsonInBytes, 0, newJsonInBytes.Length);
		}

		return appConfiguration.sourcePath;
	}

}