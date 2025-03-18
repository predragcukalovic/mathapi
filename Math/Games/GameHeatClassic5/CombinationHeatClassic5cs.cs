using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace GameHeatClassic5
{
    public class CombinationHeatClassic5 : Combination3
    {
        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'HeatClassic5'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixHeatClassic5 matrix, int numberOfLines, int bet)
        {
            Matrix = new byte[3, 5];
            NumberOfGratisGames = 0;
            GratisGame = false;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
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
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[5],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                for (var j = 0; j < 3; j++)
                {
                    lineInfo.WinningPosition[j] = (byte)(GlobalData.GameLineVegasHot[i - 1, j] * 3 + j);
                }
                lineInfo.WinningPosition[3] = 255;
                lineInfo.WinningPosition[4] = 255;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        public static Combination3 GetCombination(int numberOfLines, int bet)
        {
            var matrixArray = MatrixHeatClassic5.GetMatixArray();
            var matrix = new MatrixHeatClassic5();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationHeatClassic5();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
