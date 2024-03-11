using System.Net;
using Templates.Data.Configuration;

namespace Templates.Data.Routing;


public class CreateTestRouteHandler : RouteHandler
{
    public CreateTestRouteHandler(RouteConfiguration configuration) : base (configuration) {}

    public override bool Run(ApplicationBuilder builder)
    {
        throw new NotImplementedException();
    }
}