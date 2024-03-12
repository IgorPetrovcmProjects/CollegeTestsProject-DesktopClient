namespace Templates.Data.Routing;

using Templates.Data.Configuration;

public class CreateTestRouteHandler : RouteHandler
{
    public CreateTestRouteHandler(RouteConfiguration configuration) : base (configuration) {}

    public override bool Run(ApplicationBuilder builder)
    {
        return false;
    }
}