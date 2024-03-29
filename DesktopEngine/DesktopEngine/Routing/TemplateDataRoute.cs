﻿namespace DesktopEngine.Routing;

using System.Text.RegularExpressions;
using DesktopEngine.Exception;

public class TemplateDataRoute : IRoute
{
	private readonly Dictionary<Regex, Func<RouteHandler>> _urls;

	public TemplateDataRoute(ApplicationBuilder builder)
	{
		_urls = new Dictionary<Regex, Func<RouteHandler>>()
		{
			{ new Regex(@"\/sourcepath\/"), () => new SourcePathRouteHandler(builder.GetRouteConfiguration("/sourcepath/")) },
			{ new Regex(@"\/createtest\/"), () => new CreateTestRouteHandler(builder.GetRouteConfiguration("/createtest/")) },
			{ new Regex(@"\/updatetest\/"), () => new UpdateTestRouteHandler(builder.GetRouteConfiguration("/updatetest/")) },
			{ new Regex(@"\/deletetest\/"), () => new DeleteTestRouteHandler(builder.GetRouteConfiguration("/deletetest/")) },
			{ new Regex(@"\/gettitles\/"), () => new GetTitlesRouteHandler(builder.GetRouteConfiguration("/gettitles/")) },
			{ new Regex(@"\/gettest\/"), () => new GetTestRouteHandler(builder.GetRouteConfiguration("/gettest/"))}
		};
	}

	public RouteHandler GetRouteHandler(string urlLine)
	{
		foreach (KeyValuePair<Regex, Func<RouteHandler>> pair in _urls)
		{
			if (pair.Key.IsMatch(urlLine))
			{
				return pair.Value.Invoke();
			}
		}

		throw new InputBadFormatException("The URL was not found");
	}
}