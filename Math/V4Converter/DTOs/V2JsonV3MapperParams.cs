using Papi.GameServer.Utils.Enums;
using Newtonsoft.Json.Linq;

namespace V4Converter.DTOs
{
    public class V2JsonV3MapperParams
    {
        public JObject FrontendData { get; set; }
        public GameConfig GameConfig { get; set; }
        public int[,] Matrix { get; set; }
        public Games GameId { get; set; }
        public V2JsonV3MapperParams(JObject frontendData, GameConfig gameConfig, int[,] matrix, Games gameId)
        {
            FrontendData = frontendData;
            GameConfig = gameConfig;
            Matrix = matrix;
            GameId = gameId;
        }
    }
}
