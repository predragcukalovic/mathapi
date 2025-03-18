using GameWildSunburst;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameWildSunburstConversion
    {
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, bool isCurrentGameGratis)
        {
            var matrix = new int[5, 3];
            var recallMatrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j];
                    recallMatrix[i, j] = combination.Matrix[i, j];
                }
                var min = (isCurrentGameGratis || combination.GratisGame) ? 5 : 1;
                tmpUpperRow[i] = (int)SoftwareRng.Next(min, 9);
                tmpBottomRow[i] = (int)SoftwareRng.Next(min, 9);
            }
            var n = combination.LinesInformation.Count(x => x.Id != 253);
            foreach (var li in combination.LinesInformation)
            {
                if (li.Id == 253)
                {
                    foreach (var wp in li.WinningPosition)
                    {
                        var wpReel = wp % 5;
                        var wpRow = wp / 5;
                        recallMatrix[wpReel, wpRow] = recallMatrix[wpReel, wpRow] > 10 ? recallMatrix[wpReel, wpRow] + 1 : 11;
                    }
                }
            }
            var winLine = new WinLineV3[n];
            var ind = 0;
            var wildOverthrow = new List<WinSymbolV3>();
            foreach (var li in combination.LinesInformation)
            {
                if (li.Id != 253)
                {
                    winLine[ind] = new WinLineV3
                    {
                        lineId = li.Id,
                        soundId = li.WinningElement,
                        win = li.Win
                    };
                    var positions = new List<int>();
                    var index = 0;
                    while (index < 5 && combination.LinesInformation[ind].WinningPosition[index] != 255)
                    {
                        positions.Add(combination.LinesInformation[ind].WinningPosition[index++]);
                    }
                    var m = positions.Count;
                    var winSymb = new WinSymbolV3[m];
                    for (var j = 0; j < m; j++)
                    {
                        winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                        winSymb[j].id = recallMatrix[winSymb[j].reel, winSymb[j].row];
                    }
                    winLine[ind].symbols = winSymb;
                    ind++;
                }
                else
                {
                    foreach (var l in li.WinningPosition)
                    {
                        wildOverthrow.Add(new WinSymbolV3 { reel = l % 5, row = l / 5, id = 0 });
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
                    recall = recallMatrix,
                    overthrow = wildOverthrow.ToArray()
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = gratisGamesLeft > 0 ? new[,] { { 1, 7, 7 }, { 8, 8, 2 }, { 10, 10, 10 }, { 3, 4, 5 }, { 6, 6, 8 } } : new[,] { { 3, 8, 4 }, { 1, 2, 9 }, { 5, 0, 7 }, { 9, 2, 1 }, { 4, 6, 3 } };
            var matrix = new MatrixWildSunburst();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildSunburst();
            combination.MatrixToCombinationWildSunburst(matrix, numberOfLines, bet, gratisGamesLeft > 0);
            return combination;
        }
    }

}
