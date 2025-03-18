using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameWildHeartBeat;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameWildHeartBeatConversion
    {
        public static Combination GetNonWinningCombination(int bet)
        {
            var matrixArray = new[,] { { 8, 8, 8, 8, 8 }, { 7, 7, 7, 7, 7 }, { 6, 6, 6, 6, 6 }, { 5, 5, 5, 5, 5 }, { 4, 4, 4, 4, 4 } };
            var matrix = new MatrixWildHeartBeat();
            matrix.FromMatrixArrayWildHeartBeat(matrixArray);
            var combination = new CombinationWildHeartBeat();
            combination.MatrixToCombinationWildHeartBeat(matrix, bet);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
                tmpBottomRow[i] = combination.Matrix[i, 4];
            }
            var n = combination.LinesInformation.Length;
            var winLineList = new List<WinLineV3>();
            for (var i = 0; i < n; i++)
            {
                var wl = new WinLineV3
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
                    if (matrix[winSymb[j].reel, 0] * matrix[winSymb[j].reel, 1] * matrix[winSymb[j].reel, 2] == 0)
                    {
                        winSymb[j].id = 0;
                    }
                }
                wl.symbols = winSymb;
                winLineList.Add(wl);
            }

            var exp = new List<WildExpandV3>();
            for (var i = 0; i < 5; i++)
            {
                if (combination.PositionFor2[i] != 255)
                {
                    var wld = new WildExpandV3
                    {
                        type = "expand",
                        origin = new CoordinateV3 { reel = combination.PositionFor2[i] % 5, row = combination.PositionFor2[i] / 5 - 1 }
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
                wins = winLineList.ToArray(),
                gratisGame = false
            };

            return slotData;
        }
    }
}
