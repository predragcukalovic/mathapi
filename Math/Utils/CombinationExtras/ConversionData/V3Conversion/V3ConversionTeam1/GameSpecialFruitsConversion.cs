using GameSpecialFruits;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1
{

    public class GameSpecialFruitsConversion
    {

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = (int)SoftwareRng.Next(3, 8);
                tmpBottomRow[i] = (int)SoftwareRng.Next(3, 8);
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
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    numberOfLines = combination.AdditionalInformation,
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static Combination GetNonWinningCombination(int bet, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 1, 7, 3 }, { 5, 2, 6 }, { 1, 4, 3 }, { 0, 0, 0 }, { 5, 7, 0 } };
            var matrix = new MatrixSpecialFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationSpecialFruits();
            combination.MatrixToCombination(matrix, bet, true);
            return combination;
        }
    }
}
