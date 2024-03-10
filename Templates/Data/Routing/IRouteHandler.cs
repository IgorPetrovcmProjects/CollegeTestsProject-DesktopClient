namespace Templates.Data.Routing;

using System.Net;

public interface IRouteHandler
{
    public bool RecognizeRequest(HttpListenerRequest request);

    public bool Run(ApplicationBuilder builder);
}