﻿using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameBlowFruits40;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameBlowFruits40Conversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 8, 8, 8, 8, 2, 2 }, { 6, 6, 6, 6, 6, 6 }, { 3, 3, 1, 1, 2, 2 }, { 8, 1, 1, 8, 8, 8 }, { 4, 4, 4, 4, 4, 4 } };
            var matrix = new MatrixBlowFruits40();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBlowFruits40();
            combination.MatrixToCombination(matrix, numberOfLines, bet, false);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 4];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 5];
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

            var exp = new List<WildExpandV3>();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        var wld = new WildExpandV3
                        {
                            type = "expand",
                            origin = new CoordinateV3 { reel = i, row = j }
                        };
                        var coors = new List<CoordinateV3>();
                        for (var rl = i - 1; rl <= i + 1; rl++)
                        {
                            for (var rw = j - 1; rw <= j + 1; rw++)
                            {
                                if (rl >= 0 && rl <= 4 && rw >= 0 && rw <= 3 && !(rl == i && rw == j))
                                {
                                    coors.Add(new CoordinateV3 { reel = rl, row = rw });
                                }
                            }
                        }
                        wld.coordinates = coors.ToArray();
                        exp.Add(wld);
                    }
                }
            }

            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    wildExpand = exp.ToArray()
                },
                wins = winLine,
                gratisGame = false
            };

            return slotData;
        }
    }
}
