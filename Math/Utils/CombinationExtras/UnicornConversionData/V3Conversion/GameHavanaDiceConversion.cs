using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameHavanaDice;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameHavanaDiceConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var nearlyMissed = new int[5, 2];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = combination.Matrix[i, 0];
                nearlyMissed[i, 1] = combination.Matrix[i, 4];

                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
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
                while (index < 5 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = -1 + positions[j] / 5 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
                winLine[i].symbols = winSymb;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    nearlyMissedSymbols = nearlyMissed,
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 1, 11, 11, 11, 2 }, { 10, 10, 10, 4, 8 }, { 9, 9, 9, 1, 6 }, { 8, 8, 8, 3, 8 }, { 11, 11, 11, 2, 3 } };
            var matrix = new MatrixHavanaDice();
            matrix.FromMatrixArrayHavanaDice(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
