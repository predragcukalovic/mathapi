using Papi.GameServer.Utils.Enums;

namespace V4Converter.DTOs
{
    public class WildExpandParams
    {
        public Games GameId;
        public int[,] Matrix;
        public GameConfig GameConfig;
        public byte[] PositionFor2;

        public WildExpandParams(Games gameId, int[,] matrix, GameConfig gameConfig, byte[] positionFor2)
        {
            GameId = gameId;
            Matrix = matrix;
            GameConfig = gameConfig;
            PositionFor2 = positionFor2;
        }
    }
}
