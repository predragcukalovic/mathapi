using CombinationExtras.ReaderData;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Serilog.Events;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using V4Converter;
using V4Converter.Readers;

namespace Papi.GameServer.Math.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var minimumLevel = (LogEventLevel)Enum.Parse(
                typeof(LogEventLevel),
                ConfigurationManager.AppSettings["MinimumLoggingLevel"],
                true);

            Logger.Init(ConfigurationManager.AppSettings["LoggingDirectory"],
                "MathAPI",
                bool.Parse(ConfigurationManager.AppSettings["UseJsonLogFormatter"]),
                minimumLevel);

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            MathSlotFilesReader.ReadAllFiles(Server.MapPath(@"Data"), new Games(), ConfigurationManager.AppSettings["SoftwareVersion"]);
            UnicornFileReader.ReadAllFiles(Server.MapPath(@"DataExt"), new Games());
            GamesConfigReader.ReadGamesConfigData(Server.MapPath(@"GameConfigData/GamesConfig.json"));
            MathBuyBonusFilesReader.ReadAllFiles(Server.MapPath(@"DataBuyBonus"), new Games());
            GameConfigReader.ReadGameConfigData(ToV4Converter.getConvertedGames());
            GameLineConfigReader.ReadGameLineConfigData();
        }
    }
}