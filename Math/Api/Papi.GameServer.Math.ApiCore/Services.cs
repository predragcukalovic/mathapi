using CombinationExtras.ReaderData;
using Papi.GameServer.Math.ApiCore.Models;
using Papi.GameServer.Math.JollyPoker.PokerReader;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using System;
using V4Converter;
using V4Converter.Readers;

namespace Papi.GameServer.Math.ApiCore
{
    public static class Services
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IHostApplicationBuilder builder)
        {
            // register all services here
            services.AddScoped<JollyPokerReader>();

            var softwareVestion = builder.Configuration.GetSection("SoftwareVersion").Get<string>();


            MathSlotFilesReader.ReadAllFiles(@".\Data", new Games(), softwareVestion);
            UnicornFileReader.ReadAllFiles(@".\DataExt", new Games());
            GamesConfigReader.ReadGamesConfigData(@".\GameConfigData/GamesConfig.json");
            MathBuyBonusFilesReader.ReadAllFiles(@".\DataBuyBonus", new Games());
            GameConfigReader.ReadGameConfigData(ToV4Converter.getConvertedGames());
            GameLineConfigReader.ReadGameLineConfigData();


            return services;
        }



        public static void RegisterLogger(this IHostApplicationBuilder builder)
        {
            var configuration = builder.Configuration.GetSection("SerilogConfig")
                                .Get<SerilogConfig>();

            var minimumLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), configuration.MinimumLoggingLevel, true);
            Logger.Init(configuration.LoggingDirectory, "MathAPI", configuration.UseJsonLogFormatter, minimumLevel);
        }
    }
}
