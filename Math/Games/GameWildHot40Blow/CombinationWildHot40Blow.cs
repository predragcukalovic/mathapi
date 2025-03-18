using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Linq;

namespace GameWildHot40Blow
{
    public class CombinationWildHot40Blow : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'CrystalHot40Max' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixWildHot40Blow matrix, int numberOfLines, int bet)
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

            var lis = new LineInfo
            {
                WinningPosition = matrix.GetPositionsArray(2),
                Id = EXTRA_LINE,
                Win = matrix.GetNoLineWin(2, LineWinsForGames.WinForScatterTurboHot40) * bet * numberOfLines,
                WinningElement = 2
            };

            matrix.SetExpanding();

            GratisGame = false;
            NumberOfGratisGames = 0;

            CreateLinesInformationsTurbo(matrix, numberOfLines, bet, 0, LineWinsForGames.WinForWildsTurboHot40, GlobalData.GameLineTurbo/*, matrix.GetNoLineWin(2, LineWinsForGames.WinForScatterTurboHot40), 2*/);


            var li = LinesInformation.ToList();
            li.Insert(0, lis);
            NumberOfWinningLines++;
            TotalWin += lis.Win;
            LinesInformation = li.ToArray();
        }
    }
}
