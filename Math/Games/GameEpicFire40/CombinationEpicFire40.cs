using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameEpicFire40
{
    public class CombinationEpicFire40 : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'EpicFire40' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixEpicFire40 matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixEpicFire40.WinForWildsEpicHot40, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(2, MatrixEpicFire40.WinForScatterEpicHot40), 2);
        }
    }
}
