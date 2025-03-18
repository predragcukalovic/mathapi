using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameGoldenExplosion
{
    public class CombinationGoldenExplosion : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'GoldenExplosion' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="numberOfLines">Broj linija na koje se igra</param>
        /// <param name="bet">Ulog</param>
        public void MatrixToCombination(MatrixGoldenExplosion matrix, int numberOfLines, int bet)
        {
            FillMatrixArray(matrix);

            CreateEmptyArray(PositionFor2);
            GratisGame = false;
            NumberOfGratisGames = 0;
            var nextPosition = 0;
            var multiplier = 1;
            LineInfo li2 = null;
            var no2 = matrix.GetNumberOfElement(2);
            if (no2 >= 3)
            {
                li2 = new LineInfo
                {
                    WinningPosition = matrix.GetPositionsArray(2),
                    Id = EXTRA_LINE,
                    Win = MatrixGoldenExplosion.WinForScatterGoldenExplosion[no2 - 1] * bet * numberOfLines,
                    WinningElement = 2
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
                    var multIndex = -1;
                    multiplier = MatrixGoldenExplosion.GetRandomMultiplier(ref multIndex);
                    Matrix[2, j] = (byte)(multIndex == -1 ? 1 : 12 + multIndex);
                    matrix.SetElement(2, 0, 1);
                    matrix.SetElement(2, 1, 1);
                    matrix.SetElement(2, 2, 1);
                    break;
                }
            }

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i, multiplier);
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
            if (li2 != null)
            {
                TotalWin += li2.Win;
                linesInfo.Add(li2);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
            WinFor2 = multiplier;
        }
    }
}
