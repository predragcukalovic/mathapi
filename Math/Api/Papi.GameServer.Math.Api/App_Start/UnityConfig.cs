using Papi.GameServer.Math.JollyPoker.PokerReader;
using Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Papi.GameServer.Math.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            //var services = new ServiceCollection();
            //services.AddScoped<JollyPokerReader>();
            //var serviceProvider = services.BuildServiceProvider();
            //container.RegisterInstance(serviceProvider.GetService<JollyPokerReader>());
            container.RegisterType<JollyPokerReader>();
            container.RegisterType<AdditionalGameDataService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}