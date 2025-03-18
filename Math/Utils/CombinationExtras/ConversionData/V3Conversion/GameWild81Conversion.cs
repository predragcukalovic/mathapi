using GameWild81;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameWild81Conversion
    {
        public static Combination GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 9, 9, 8, 7, 7 }, { 7, 0, 1, 2, 3 }, { 5, 6, 5, 10, 4 }, { 1, 4, 3, 9, 8 } };
            var matrix = new MatrixWild81();
            matrix.FromMatrixArrayWild81(matrixArray);
            var combination = new CombinationWild81();
            combination.MatrixToCombinationWild81(matrix, bet);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[4, 3];
            var tmpUpperRow = new int[4];
            var tmpBottomRow = new int[4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 4];
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
                while (index < 4 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymb = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 4, row = positions[j] / 4 };
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
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    mysteryWin = combination.WinFor2
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }
    }
}
