using CombinationUnicornUtils;
using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameMiniMegaCash;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameMiniMegaCashConversion
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 - 1 };
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                }
                winLine[i].symbols = winSymb;
            }

            var mult = new[] { 1, 2, 3, 5 };
            var bMult = new[] { 40, 10, 5 };
            var ant = 0;
            var potentialMult1 = (matrix[1, 0] > 9 ? mult[(matrix[1, 0] - 10) % 4] : 0) + (matrix[1, 1] > 9 ? mult[(matrix[1, 1] - 10) % 4] : 0) + (matrix[1, 2] > 9 ? mult[(matrix[1, 2] - 10) % 4] : 0);
            var potentialMult2 = (matrix[2, 0] > 9 ? mult[(matrix[2, 0] - 10) % 4] : 0) + (matrix[2, 1] > 9 ? mult[(matrix[2, 1] - 10) % 4] : 0) + (matrix[2, 2] > 9 ? mult[(matrix[2, 2] - 10) % 4] : 0);
            var potentialMultA = (matrix[1, 0] > 9 ? bMult[(matrix[1, 0] - 10) / 4] : 0) + (matrix[1, 1] > 9 ? bMult[(matrix[1, 1] - 10) / 4] : 0) + (matrix[1, 2] > 9 ? bMult[(matrix[1, 2] - 10) / 4] : 0);
            var potentialMultB = (matrix[2, 0] > 9 ? bMult[(matrix[2, 0] - 10) / 4] : 0) + (matrix[2, 1] > 9 ? bMult[(matrix[2, 1] - 10) / 4] : 0) + (matrix[2, 2] > 9 ? bMult[(matrix[2, 2] - 10) / 4] : 0);
            var potMult = potentialMult1 * potentialMult2 * 5 * ((potentialMultA == potentialMultB) ? potentialMultA : 2);
            if (potMult > 18)
            {
                ant = 2;
            }
            else if (potMult > 0)
            {
                ant = 1;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    nearlyMissedSymbols = nearlyMissed,
                    anticipation = ant
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 3, 9, 9, 9, 4 }, { 5, 8, 8, 8, 6 }, { 7, 7, 7, 7, 8 }, { 4, 6, 6, 6, 3 }, { 9, 5, 5, 5, 5 } };
            var matrix = new MatrixMiniMegaCash();
            matrix.FromMatrixArrayMiniMegaCash(matrixArray);
            var combination = new CombinationUnicornNew();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
