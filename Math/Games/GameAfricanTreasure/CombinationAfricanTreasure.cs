using MathCombination.CombinationData;
using System.Collections.Generic;

namespace GameAfricanTreasure
{
    public class CombinationAfricanTreasure : Combination
    {
        /// <summary>
        /// Transformiše matricu za igru 'AfricanTreasure' u kombinaciju
        /// </summary>
        /// <param name="matrix">Matrica sa kojom se radi</param>
        /// <param name="bet">Ulog</param>
        /// <param name="numberOfLines"></param>
        public void MatrixToCombinationAfricanTreasure(MatrixAfricanTreasure matrix, int bet, int numberOfLines)
        {
            GratisGame = false;
            NumberOfGratisGames = 0;
            Matrix = new byte[5, 5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = (byte)matrix.GetElement(i, j);
                }
            }

            CreateEmptyArray(MultiplyFor2);
            CreateEmptyArray(GratisGamesValues);
            CreateEmptyArray(GratisGamesPositions);

            TotalWin = 0;
            var linesInfo = new List<LineInfo>();
            for (var i = 1; i <= numberOfLines; i++)
            {
                var win = matrix.CalculateWinLine(i);
                if (win == 0)
                {
                    continue;
                }
                var lineInfo = new LineInfo
                {
                    Id = (byte)(i - 1),
                    Win = win * bet,
                    WinningElement = (byte)matrix.GetWinningElementForLine(i)
                };
                lineInfo.WinningPosition = matrix.GetLinePositions(i, lineInfo.WinningElement);
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            var scatterWin = matrix.GetScatterWin();
            if (scatterWin > 0)
            {
                var lineInfo = new LineInfo
                {
                    WinningPosition = matrix.GetScatterPositionsArray(),
                    Id = EXTRA_LINE,
                    Win = scatterWin * bet * numberOfLines,
                    WinningElement = 2
                };
                TotalWin += lineInfo.Win;
                linesInfo.Add(lineInfo);
            }
            NumberOfWinningLines = (byte)linesInfo.Count;
            LinesInformation = linesInfo.ToArray();
        }

        public static Combination GetCombinationAfricanTreasure(int numberOfLines, int bet)
        {
            var matrixArray = MatrixAfricanTreasure.GetMatixArray();
            var matrix = new MatrixAfricanTreasure();
            matrix.FromMatrixArrayAfricanTreasure(matrixArray);
            var combination = new CombinationAfricanTreasure();
            combination.MatrixToCombinationAfricanTreasure(matrix, bet, numberOfLines);
            return combination;
        }
    }
}
