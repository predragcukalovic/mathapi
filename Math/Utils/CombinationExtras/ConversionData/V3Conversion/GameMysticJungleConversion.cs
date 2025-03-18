using GameMysticJungle;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameMysticJungleConversion
    {
        public static Combination GetNonWinningCombination(int numberOfLines, int bet)
        {
            var matrixArray = new[,] { { 8, 8, 8, 5, 5 }, { 7, 7, 0, 0, 0 }, { 1, 1, 1, 6, 6 }, { 2, 2, 2, 4, 4 }, { 8, 8, 3, 3, 3 } };
            var matrix = new MatrixMysticJungle();
            matrix.FromMatrixArrayMysticJungle(matrixArray);
            var combination = new CombinationMysticJungle();
            combination.MatrixToCombinationMysticJungle(matrix, numberOfLines, bet);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var mys = combination.WinFor2;
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j] + (combination.Matrix[i, j] == 9 ? mys : 0);
                }
                tmpUpperRow[i] = combination.Matrix[i, 0] + (combination.Matrix[i, 0] == 9 ? mys : 0);
                tmpBottomRow[i] = combination.Matrix[i, 4] + (combination.Matrix[i, 4] == 9 ? mys : 0);
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
                    winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row] >= 9 ? mys : matrix[winSymb[j].reel, winSymb[j].row];
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
                    bottomRow = tmpBottomRow
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
