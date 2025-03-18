using MathCombination.CombinationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWild5
{
    public class CombinationWild5 : Combination3
    {
        public void MatrixToCombination(MatrixWild5 matrix, int bet)
        {
            NumberOfGratisGames = 0;
            GratisGame = false;
            Matrix = matrix.GetMatrix();

            var dbl = matrix.DoubleWin() ? 2 : 1;

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 5; i++)
            {
                var winOfLine = matrix.GetLineWinForWild5(i, out int winningElement);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet * dbl;
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetWinningPositions(i),
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = (byte)winningElement
                };
                TotalWin += win;
                linesInfo.Add(lineInfo);
            }

            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            WinFor2 = dbl - 1;
        }
    }
}