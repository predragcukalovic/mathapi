using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameWild27
{
    public class CombinationWild27 : Combination3
    {
        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'Wild27'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixWild27 matrix, int bet)
        {
            NumberOfGratisGames = 0;
            GratisGame = false;
            Matrix = matrix.GetMatrix();

            var dbl = matrix.DoubleWin() ? 2 : 1;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 27; i++)
            {
                var winOfLine = matrix.GetLineWin(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet * dbl;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositionsCrissCross(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            WinFor2 = dbl - 1;
        }
    }
}
