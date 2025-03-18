using GameWildClover506;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    class GameWildClover506Conversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 10, 10, 10, 10, 10, 10 }, { 9, 9, 9, 9, 9, 9 }, { 7, 7, 7, 7, 7, 7 }, { 8, 8, 8, 8, 8, 8 }, { 6, 6, 6, 6, 6, 6 }, { 4, 4, 4, 4, 4, 4 } };
            var matrix = new Matrix40WildClover6();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[6, 4];
            var tmpUpperRow = new int[6];
            var tmpBottomRow = new int[6];
            for (var i = 0; i < 6; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 5];
            }
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement,
                    win = combination.LinesInformation[i].Win
                };
                var positions = new List<int>();
                var index = 0;
                while (index < 6 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymbol = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymbol[j] = new WinSymbolV3 { reel = positions[j] % 6, row = positions[j] / 6 };
                    winSymbol[j].id = matrix[winSymbol[j].reel, winSymbol[j].row];
                }
                winLine[i].symbols = winSymbol;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
