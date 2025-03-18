using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace CombinationExtras.ReaderData
{
    public static class GamesConfigReader
    {
        private static Dictionary<string, Dictionary<string, decimal>> _GamesConfigData = new Dictionary<string, Dictionary<string, decimal>>();
        public static void ReadGamesConfigData(string path)
        {
            var json = File.ReadAllText(path);
            _GamesConfigData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(json);
        }

        public static decimal GetRtp(string game)
        {
            if (_GamesConfigData.TryGetValue(game, out Dictionary<string, decimal> gameConfig))
            {
                if (gameConfig.TryGetValue("rtp", out decimal rtp))
                    return rtp;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
    }

}
