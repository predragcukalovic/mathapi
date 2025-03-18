using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameSimplyRunner
{
    public class CombinationSimplyRunner : Combination3
    {
        /// <summary>
        /// Kreira dodatne informacije o linijama
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        private void CreateLinesInformationsSimplyRunner(MatrixSimplyRunner matrix, int numberOfLines, int bet)
        {
            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var winOfLine = matrix.CalculateWinLine(i);
                if (winOfLine == 0)
                    continue;
                var win = winOfLine * bet;
                var winningElement = (byte)matrix.GetWinningElementForLine(i);
                var lineInfo = new LineInfo
                {
                    WinningPosition = new byte[3],
                    Id = (byte)(i - 1),
                    Win = win,
                    WinningElement = winningElement
                };
                TotalWin += win;
                MatrixSimplyRunner.CreateWinningLinePositions(ref lineInfo.WinningPosition, i);
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        /// <summary>
        /// Pretvara matricu u kombinaciju za igru 'SimplyRunner'
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="bet"></param>
        public void MatrixToCombination(MatrixSimplyRunner matrix, int numberOfLines, int bet)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[6, 5];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }
            CreateLinesInformationsSimplyRunner(matrix, numberOfLines, bet);
        }

        public static Combination3 GetCombination(int numberOfLines, int bet, bool ultra)
        {
            var matrixArray = MatrixSimplyRunner.GetMatrixArray(ultra);
            var matrix = new MatrixSimplyRunner();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSimplyRunner();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
