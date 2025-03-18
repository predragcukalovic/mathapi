using MathCombination.CombinationData;
using System.Collections.Generic;

namespace V4Converter.DTOs
{
    public class WinSymbolMapperParams
    {
        public List<int> Positions { get; set; }
        public int[,] Matrix { get; set; }
        public ICombination Combination { get; set; }
        public GameConfig GameConfig { get; set; }

        public WinSymbolMapperParams(List<int> positions, int[,] matrix, ICombination combination, GameConfig gameConfig)
        {
            Positions = positions;
            Matrix = matrix;
            Combination = combination;
            GameConfig = gameConfig;
        }
    }
}
