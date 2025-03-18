using Papi.GameServer.Math.Api.DelegatingHandlers;
using Papi.GameServer.Math.Api.Exceptions;
using Prometheus.AspNet;
using System.Web.Http;

namespace Papi.GameServer.Math.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new TraceIdHandler());
            config.Filters.Add(new ExceptionFilter());
            PrometheusConfig.UseMetricsServer(config);
        }
    }
}
