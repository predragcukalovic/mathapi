using System.Collections.Generic;
using MathCombination.CombinationData;

namespace GameTopHot5
{
    public class CombinationTopHot5 : Combination3
    {
        public void MatrixToCombination(MatrixTopHot5 matrix, int numberOfLines, int bet)
        {
            NumberOfGratisGames = 0;
            Matrix = matrix.GetMatrix();
            CreateLinesInformations(matrix, bet);
        }

        protected void CreateLinesInformations(MatrixTopHot5 matrix, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 5; i++)
            {
                var winOfLine = matrix.CalculateWinOfLine(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetWinningPositions(i),
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
