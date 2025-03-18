using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameWildHeat40
{
    public class CombinationWildHeat40 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'WildHeat40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationWildHeat(MatrixWildHeat40 matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            matrix.FixWilds();

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsTurboHot40, GlobalData.GameLineTurbo);
        }
    }
}
