namespace DesktopEngine.Routing;


public interface IRoute
{
    public RouteHandler GetRouteHandler(string urlLine);
}