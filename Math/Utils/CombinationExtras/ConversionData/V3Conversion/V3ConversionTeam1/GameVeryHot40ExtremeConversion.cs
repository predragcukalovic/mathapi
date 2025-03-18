using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameVeryHot40Extreme;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1
{
    public class GameVeryHot40ExtremeConversion
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
                tmpUpperRow[i] = (int)SoftwareRng.Next(2, 9);
                tmpBottomRow[i] = (int)SoftwareRng.Next(2, 9);
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
                    if (matrix[winSymb[j].reel, 0] == 0 || matrix[winSymb[j].reel, 1] == 0 || matrix[winSymb[j].reel, 2] == 0)
                    {
                        winSymb[j].id = 0;
                    }
                    if (matrix[winSymb[j].reel, 0] == 1 || matrix[winSymb[j].reel, 1] == 1 || matrix[winSymb[j].reel, 2] == 1)
                    {
                        winSymb[j].id = 1;
                    }
                }
                winLine[i].symbols = winSymb;
            }

            var exp = new List<WildExpandV3>();
            for (var i = 0; i < 5; i++)
            {
                if (combination.PositionFor2[i] < 15)
                {
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = combination.PositionFor2[i] % 5, row = combination.PositionFor2[i] / 5 }
                    };
                    var coors = new List<CoordinateV3>();
                    for (var j = 0; j < 3; j++)
                    {
                        if (j != wld.origin.row)
                        {
                            coors.Add(new CoordinateV3 { reel = wld.origin.reel, row = j });
                        }
                    }
                    wld.coordinates = coors.ToArray();
                    exp.Add(wld);
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
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static Combination GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 4, 10, 5 }, { 9, 2, 8 }, { 6, 6, 6 }, { 7, 7, 4 }, { 6, 6, 8 } };
            var matrix = new MatrixVeryHot40Extreme();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationVeryHot40Extreme();
            combination.MatrixToCombination(matrix, 4, bet);
            return combination;
        }
    }
}