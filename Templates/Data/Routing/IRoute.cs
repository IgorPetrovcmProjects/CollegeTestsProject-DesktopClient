namespace Templates.Data.Routing;


public interface IRoute
{

    public IRouteHandler GetRouteHandler(string urlLine);
}