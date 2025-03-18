using Papi.GameServer.Utils.Enums;
using MathCombination.CombinationData;
using V4Converter.DTOs;

namespace V4Converter
{
    public class V3MapperParams
    {
        public ICombination Combination;
        public int NumberOfReels;
        public int NumberOfRows;
        public Games GameId;
        public int[,] Matrix;
        public long Bet;
        public bool IsCurrentGameGratis;
        public GameConfig GameConfig;
        public int GratisGamesLeft;

        public V3MapperParams(ICombination combination, Games gameId, int[,] matrix, long bet, bool isCurrentGameGratis, GameConfig gameConfig, int gratisGamesLeft)
        {
            Combination = combination;
            NumberOfReels = gameConfig.NumberOfReels;
            NumberOfRows = gameConfig.NumberOfRows;
            GameId = gameId;
            Matrix = matrix;
            Bet = bet;
            IsCurrentGameGratis = isCurrentGameGratis;
            GameConfig = gameConfig;
            GratisGamesLeft = gratisGamesLeft;
        }

        public void setMatrix(int[,] matrix)
        {
            Matrix = matrix;
        }
    }
}
