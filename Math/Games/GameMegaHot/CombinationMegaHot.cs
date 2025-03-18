
using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameMegaHot
{
    public class CombinationMegaHot : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'MegaHot10' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="bet">Broj linija</param>
        public void MatrixToCombinationMegaHot10(MatrixMegaHot10 matrix, int bet, int numberOfLines)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            WinFor2 = matrix.GetMultiplicator();
            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, WinFor2, -1, null, GlobalData.GameLineExtra);
        }
    }
}
