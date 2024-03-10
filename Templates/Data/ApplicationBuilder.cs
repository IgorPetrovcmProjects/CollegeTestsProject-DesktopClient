namespace Templates.Data;

using System.Text;
using System.Net;
using Newtonsoft.Json;
using Templates.Data.Configuration;
using Templates.Data.Exception;

public class ApplicationBuilder
{
    private Dictionary<string, RouteConfiguration> configurations = new Dictionary<string, RouteConfiguration>();

    private HashSet<int> finalStatusCodes = new HashSet<int>();
    public HashSet<int> FinalStatusCodes { get { return finalStatusCodes; }}

    private const string PathToRouteConfigurationFile = "Configuration/route-configuration.json"; 

    private const string PathToAppConfigurationFile = "Configuration/configuration.json";

    private const string Url = "http://127.0.0.1:3330";

    private string sourcePath;

    private static HttpListener listener = new HttpListener();
    public HttpListener Listener { get { return listener; } }

    private HttpListenerContext _context;

    public HttpListenerContext Context { get { return _context; } }

    private void AssignConfigurationForUrls()
    {
        FileStream fs = new FileStream(PathToRouteConfigurationFile, FileMode.Open, FileAccess.ReadWrite);

        byte[] jsonInBytes = new byte[fs.Length];

        fs.Read(jsonInBytes, 0, jsonInBytes.Length);

        string json = Encoding.UTF8.GetString(jsonInBytes);

        configurations = JsonConvert.DeserializeObject<Dictionary<string, RouteConfiguration>>(json);

        fs.Close();
        fs.Dispose();
    }

    private void AssignRoutes()
    {
        if (configurations.Count == 0){
            throw new ApplicationRoutesException("The Routes was not found");
        }
        foreach (string key in configurations.Keys)
        {   
            listener.Prefixes.Add(Url + key);
        }
    }

    public ApplicationBuilder()
    {
        listener = new HttpListener();

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

    public void Stop()
    {
        listener.Stop();
    }

    public void Close()
    {
        listener.Close();
    }

    public bool SetSourceDirectory(string path)
    {
        if (Directory.Exists(path)){
            ApplicationConfiguration appConfiguration = new ApplicationConfiguration() {
            sourcePath = path
            };

            FileStream fs = new FileStream(PathToAppConfigurationFile, FileMode.Open, FileAccess.ReadWrite);

            byte[] jsonInBytes = new byte[fs.Length];

            fs.Read(jsonInBytes, 0, jsonInBytes.Length);

            string json = Encoding.UTF8.GetString(jsonInBytes);

            ApplicationConfiguration currentAppConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(json);

            currentAppConfiguration = appConfiguration;

            fs.SetLength(0);

            json = JsonConvert.SerializeObject(currentAppConfiguration);

            jsonInBytes = Encoding.UTF8.GetBytes(json);

            fs.Write(jsonInBytes, 0, jsonInBytes.Length);

            fs.Close();
            fs.Dispose();

            return true;
        }

        return false;
    }

}