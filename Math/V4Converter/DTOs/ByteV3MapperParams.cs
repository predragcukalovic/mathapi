using Papi.GameServer.Utils.Enums;

namespace V4Converter.DTOs
{
    public class ByteV3MapperParams
    {
        public byte[] FrontendData { get; set; }
        public GameConfig GameConfig { get; set; }
        public int[,] Matrix { get; set; }
        public Games GameId { get; set; }
        public ByteV3MapperParams(byte[] frontendData, GameConfig gameConfig, int[,] matrix, Games gameId)
        {
            FrontendData = frontendData;
            GameConfig = gameConfig;
            Matrix = matrix;
            GameId = gameId;
        }
    }
}
