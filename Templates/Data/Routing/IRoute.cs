namespace Templates.Data.Routing;


public interface IRoute
{
    public RouteHandler GetRouteHandler(string urlLine);
}