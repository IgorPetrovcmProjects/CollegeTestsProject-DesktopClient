namespace Templates.Data.Routing;

using System.Text.RegularExpressions;
using Templates.Data.Exception;

public class TemplateDataRoute : IRoute
{
    private readonly static Dictionary<Regex, Func<IRouteHandler>> _urls = new Dictionary<Regex, Func<IRouteHandler>>()
    {
        { new Regex(@"\/sourcepath\/"), () => new SourthpathRoute() }
    };

    public IRouteHandler GetRouteHandler(string urlLine)
    {
        foreach (KeyValuePair<Regex, Func<IRouteHandler>> pair in _urls)
        {
            if (pair.Key.IsMatch(urlLine))
            {
                return pair.Value.Invoke();
            }
        }

        throw new InputBadFormatException("The URL was not found");
    }
}