using MathCombination.CombinationData;

namespace GameHotRushFruitLines
{
    public class CombinationHotRushFruitLines : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'HotRushFruitLines' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationHotRushFruitLines(MatrixHotRushFruitLines matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            CreateLinesInformations(matrix, 5, bet, 1, -1, null, MatrixHotRushFruitLines.GameLineHotRushFruitLines);
        }
    }
}
