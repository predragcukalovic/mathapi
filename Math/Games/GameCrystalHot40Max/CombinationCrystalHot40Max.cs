using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameCrystalHot40Max
{
    public class CombinationCrystalHot40Max : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'CrystalHot40Max' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixCrystalHot40Max matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[5, 6];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            matrix.SetExpanding();

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, MatrixCrystalHot40Max.WinForWildsCrystalHot40Max, GlobalData.GameLineTurbo,
                matrix.GetNoLineWin(2, MatrixCrystalHot40Max.WinForScatterCrystalHot40Max), 2);

            var winLines = LinesInformation.Count(x => x.Id != EXTRA_LINE);
            PositionFor2 = new byte[5];
            if (winLines > 0)
            {
                for (var i = 0; i < 5; i++)
                {
                    PositionFor2[i] = (byte)(matrix.GetElement(i, 1) == 0 ? 1 : 0);
                }
            }
        }
    }
}
