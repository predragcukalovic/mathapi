﻿using CombinationUtils.UnicornCombinationData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForUnicornGames.GameWildParadice;
using System.Collections.Generic;

namespace CombinationExtras.UnicornConversionData.V3Conversion
{
    public class GameWildParadiceConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var hasWild0 = new bool[5];
            for (var i = 0; i < 5; i++)
            {
                hasWild0[i] = false;
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                    if (matrix[i, j] == 0)
                    {
                        hasWild0[i] = true;
                    }
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
                    winSymb[j].id = (hasWild0[winSymb[j].reel] ? 0 : matrix[winSymb[j].reel, winSymb[j].row]);
                }
                winLine[i].symbols = winSymb;
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = null,
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }

        public static CombinationUnicorn GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 9, 9, 1 }, { 8, 8, 8 }, { 2, 2, 7 }, { 7, 3, 7 }, { 1, 7, 7 } };
            var matrix = new MatrixWildParadice();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationUnicorn();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }
    }
}
