using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameBonusEpicCrown;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameBonusEpicCrownConversion
    {
        public static Combination GetNonWinningCombination(int bet, bool gratisGame, int numberOfLines, int bonusNumberOfLines)
        {
            var matrixArray = new[,] { { 2, 6, 8 }, { 1, 4, 0 }, { 5, gratisGame ? 7 : 9, 3 }, { 0, 3, 2 }, { 4, 1, 7 } };
            var matrix = new MatrixBonusEpicCrown();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationBonusEpicCrown();
            combination.MatrixToCombinationBonusEpicCrown(matrix, bet, gratisGame, numberOfLines, bonusNumberOfLines);
            return combination;
        }

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
                tmpUpperRow[i] = (combination.Matrix[i, 0] + 5) % 8 + 1;
                tmpBottomRow[i] = (combination.Matrix[i, 2] + 5) % 8 + 1;
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
                    winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
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
                wins = winLineList.ToArray(),
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
