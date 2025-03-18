using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameHeatDouble
{
    public class CombinationHeatDouble : Combination3
    {
        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'HeatDouble'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixHeatDouble matrix, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[3, 5];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            TotalWin = 0;
            var dbl = matrix.DoubleWin();
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= 5; i++)
            {
                var winOfLine = matrix.GetLineWin(i, out int winningElement);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet * (dbl ? 2 : 1);
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
            WinFor2 = dbl ? 1 : 0;
        }

        /// <summary>
        /// Daje kombinaciju za igru HeatDouble.
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        public static ICombination GetCombinationHeatDouble(int bet)
        {
            var matrix = new MatrixHeatDouble();
            var matrixArray = MatrixHeatDouble.GetMatixArray();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationHeatDouble();
            combination.MatrixToCombination(matrix, bet);
            return combination;
        }
    }
}
