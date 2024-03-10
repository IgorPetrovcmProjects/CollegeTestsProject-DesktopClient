namespace Templates.Data;

using System.Text;
using System.Net;
using Newtonsoft.Json;
using Templates.Data.Configuration;
using Templates.Data.Exception;

public class ApplicationBuilder
{
    private Dictionary<string, RouteConfiguration> configurations = new Dictionary<string, RouteConfiguration>();

    private const string PathToRouteConfigurationFile = "Configuration/route-configuration.json"; 

    private const string Url = "http://127.0.0.1:3330";

    private string sourcePath;

    private static HttpListener listener = new HttpListener();

    private HttpListenerContext _context;

    public HttpListenerContext Context { get { return _context; } }

    private void AssignConfigurationForUrls()
    {
        FileStream fs = new FileStream(PathToRouteConfigurationFile, FileMode.Open, FileAccess.ReadWrite);

        byte[] jsonInBytes = new byte[fs.Length];

        fs.Read(jsonInBytes, 0, jsonInBytes.Length);

        string json = Encoding.UTF8.GetString(jsonInBytes);

        configurations = JsonConvert.DeserializeObject<Dictionary<string, RouteConfiguration>>(json);
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
        AssignConfigurationForUrls();
        AssignRoutes();

        listener.Start();

        _context = listener.GetContext();
    }

    public RouteConfiguration GetRouteConfiguration(string route)
    {
        return configurations[route];
    }

}