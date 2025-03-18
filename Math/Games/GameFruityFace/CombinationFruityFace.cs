using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameFruityFace
{
    public class CombinationFruityFace : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'FruityFace' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombination(MatrixFruityFace matrix, int bet, int numberOfLines)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            FillMatrixArray(matrix);

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            CreateLinesInformations(matrix, numberOfLines, bet, 1, -1, null, GlobalData.GameLineExtra);
        }

        public static Combination GetCombination(int numberOfLines, int bet)
        {
            var matrixArray = MatrixFruityFace.GetMatixArray();
            var matrix = new MatrixFruityFace();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationFruityFace();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }
    }
}
