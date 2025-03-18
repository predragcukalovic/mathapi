namespace Papi.GameServer.Math.ApiCore.Models
{
    public class SerilogConfig
    {
        public string LoggingDirectory { get; set; }
        public bool UseJsonLogFormatter { get; set; }
        public string MinimumLoggingLevel { get; set; }
    }
}
