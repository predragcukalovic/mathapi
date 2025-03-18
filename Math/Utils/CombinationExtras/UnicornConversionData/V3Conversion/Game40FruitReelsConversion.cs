using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.Game40FruitReels;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    class Game40FruitReelsConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 4];
            var nearlyMissed = new int[5, 2];

            for (var i = 0; i < 5; i++)
            {
                nearlyMissed[i, 0] = combination.Matrix[i, 5];
                nearlyMissed[i, 1] = combination.Matrix[i, 4];

                for (var j = 0; j < 4; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
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
            var matrixArray = new[,] { { 3, 3, 3, 3, 3, 3 }, { 2, 2, 2, 2, 2, 2 }, { 4, 4, 4, 4, 4, 4 }, { 6, 6, 6, 6, 6, 6 }, { 5, 5, 5, 5, 5, 5 } };
            var matrix = new Matrix40FruitReels();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, bet, numberOfLines);
            return combination;
        }
    }
}
