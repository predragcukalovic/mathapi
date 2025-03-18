using GameAquaFlame;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam2
{
    public class GameAquaFlameConversion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
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
                tmpUpperRow[i] = (matrix[i, 0] == 1 || matrix[i, 0] == 9) ? (int)SoftwareRng.Next(1, 7) + matrix[i, 0] : matrix[i, 0];
                tmpBottomRow[i] = (matrix[i, 2] == 1 || matrix[i, 2] == 9) ? (int)SoftwareRng.Next(1, 7) + matrix[i, 2] : matrix[i, 2];
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
                    aquaFlame = combination.WinFor2
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <returns></returns>
        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 6, 6, 6 }, { 4, 4, 4 }, { 5, 5, 5 }, { 3, 3, 3 }, { 7, 7, 7 } };
            var matrix = new MatrixAquaFlame();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationAquaFlame();
            combination.MatrixToCombinationAquaFlame(matrix, numberOfLines, bet, 0);
            return combination;
        }
    }
}
