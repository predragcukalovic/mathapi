using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameTripleFieldsOfLuck
{
    public class CombinationTripleFieldsOfLuck : Combination3
    {
        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'TripleFieldsOfLuck'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombinationTripleFieldsOfLuck(MatrixTripleFieldsOfLuck matrix, int numberOfLines, int bet)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            PositionFor2 = new byte[5] { 255, 255, 255, 255, 255 };
            GratisGame = false;
            NumberOfGratisGames = 0;

            var nextPosition = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 3 + i);
                        matrix.SetElement(i, 0, 0);
                        matrix.SetElement(i, 1, 0);
                        matrix.SetElement(i, 2, 0);
                        break;
                    }
                }
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinOfLine(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLineTripleFieldsOfLuck(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                CreateWinningLinePositions(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }
    }
}
