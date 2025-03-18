using Papi.GameServer.Utils.Logging;
using MathCombination.CombinationData;
using Newtonsoft.Json;

namespace CombinationExtras.Logging
{
    public static class CombinationLogger
    {
        public static void LogCombination(this ICombination combination)
        {
            var matrix = JsonConvert.SerializeObject(combination.Matrix);
            Logger.LogDebug("Matrix: " + matrix);
            var scatter = JsonConvert.SerializeObject(combination.WinFor2);
            Logger.LogDebug("Scatter: " + scatter);
            var lines = JsonConvert.SerializeObject(combination.LinesInformation, Formatting.Indented);
            Logger.LogDebug("Lines information: " + lines);
        }
    }
}