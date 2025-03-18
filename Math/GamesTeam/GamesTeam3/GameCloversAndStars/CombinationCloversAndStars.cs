using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GameCloversAndStars
{
    public class CombinationCloversAndStars : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'CloversAndStars' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombinationCloversAndStars(MatrixCloversAndStars matrix, int numberOfLines, int bet)
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

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixCloversAndStars.WinForWildCloversAndStars, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(2, MatrixCloversAndStars.WinForScatterCloversAndStars), 2);
        }
    }
}
