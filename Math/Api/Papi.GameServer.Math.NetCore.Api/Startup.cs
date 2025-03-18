using CombinationExtras.ReaderData;
using Papi.GameServer.Math.Api;
using Papi.GameServer.Utils.Enums;
using Papi.GameServer.Utils.Logging;
using Serilog.Events;
using System.Web.Mvc;

namespace Papi.GameServer.Math.NetCore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
            services.AddRouting();
            services.AddSwaggerGen();

            var minimumLevel = (LogEventLevel)Enum.Parse(
                typeof(LogEventLevel),
                Configuration["MinimumLoggingLevel"].ToString(),
                true);

            Logger.Init(Configuration["LoggingDirectory"].ToString(),
                "MathAPI",
                bool.Parse(Configuration["UseJsonLogFormatter"].ToString()),
                minimumLevel);

            //AreaRegistration.RegisterAllAreas();
            //UnityConfig.RegisterComponents();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            MathSlotFilesReader.ReadAllFiles(
                Configuration["DataPath"].ToString(),
                new Games(),
                Configuration["SoftwareVersion"].ToString());

            UnicornFileReader.ReadAllFiles(
                Configuration["DataExtPath"].ToString(), new Games());
        }
    }
}
