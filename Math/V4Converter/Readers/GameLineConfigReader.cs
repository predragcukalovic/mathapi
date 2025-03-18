using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace V4Converter.Readers
{
    public static class GameLineConfigReader
    {
        private static Dictionary<string, int[][]> _gameLineConfigData = new Dictionary<string, int[][]>();

        public static void ReadGameLineConfigData()
        {
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6), "GameConfiguration", "HelpConfig", "gameLineConfig.json");
            var json = File.ReadAllText(configPath);
            _gameLineConfigData = JsonConvert.DeserializeObject<Dictionary<string, int[][]>>(json);
        }

        public static int[][] GetGameLineConfig(string lineType)
        {
            return _gameLineConfigData.ContainsKey(lineType) ? _gameLineConfigData[lineType] : null;
        }
    }
}
