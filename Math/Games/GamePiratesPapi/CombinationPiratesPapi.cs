using MathCombination.CombinationData;
using MathForGames.BasicGameData;

namespace GamePiratesPapi
{
    public class CombinationPiratesPapi : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'PiratesPapi' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixPiratesPapi matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            CreateEmptyArray(PositionFor2);
            var index = 0;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                    if (j < 4 && Matrix[i, j] == 0)
                    {
                        PositionFor2[index++] = (byte)(j * 5 + i);
                    }
                }
            }

            matrix.SetExpanding();

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixPiratesPapi.WinForWildsPiratesPapi, GlobalData.GameLineTurbo);
        }
    }
}
