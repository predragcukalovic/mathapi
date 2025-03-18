using GameVeryHot5Extreme;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameVeryHot40Extreme
{
    public class CombinationVeryHot40Extreme : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'VeryHot40Extreme' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixVeryHot40Extreme matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            var nextPosition = 0;
            LineInfo li9 = null;
            var no9 = matrix.GetNumberOfElement(10);
            if (no9 >= 3)
            {
                li9 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(10),
                    Id = EXTRA_LINE,
                    Win = MatrixVeryHot5Extreme.WinForScatterVeryHotExtreme[no9 - 1] * bet * numberOfLines,
                    WinningElement = 10
                };
            }
            for (var i = 1; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix.GetElement(i, j) == 0)
                    {
                        PositionFor2[nextPosition++] = (byte)(j * 5 + i);
                        matrix.SetElement(i, 0, 0);
                        matrix.SetElement(i, 1, 0);
                        matrix.SetElement(i, 2, 0);
                        break;
                    }
                }
            }
            for (var j = 0; j < 3; j++)
            {
                if (matrix.GetElement(2, j) == 1)
                {
                    PositionFor2[nextPosition] = (byte)(j * 5 + 2);
                    matrix.SetElement(2, 0, 1);
                    matrix.SetElement(2, 1, 1);
                    matrix.SetElement(2, 2, 1);
                    break;
                }
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 40; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }

                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet
                };
                var lin = matrix.GetLine(i, GlobalData.GameLineExtra);
                lineInfo.WinningElement = (byte)lin.GetElement(0);
                if (lin.GetElement(2) == 1)
                {
                    lin.SetElement(2, 0);
                }
                lineInfo.WinningPosition = lin.GetLinesPositions(GlobalData.GameLineExtra, i, 0, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            if (li9 != null)
            {
                TotalWin += li9.Win;
                linesInfo.Add(li9);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            PositionFor2 = matrix.FixExpand(LinesInformation, PositionFor2);
        }
    }
}
