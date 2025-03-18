using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameCrownOfSecret;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion.V3ConversionTeam1
{
    public class GameCrownOfSecretConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, bool gratisGame)
        {
            var matrixArray = gratisGame
                ? new[,] { { 2, 6, 8 }, { 1, 4, 5 }, { 5, 6, 3 }, { 5, 3, 2 }, { 4, 1, 7 } }
                : new[,] { { 2, 9, 3 }, { 0, 1, 2 }, { 8, 9, 1 }, { 0, 1, 4 }, { 4, 5, 2 } };

            var matrix = new MatrixCrownOfSecret();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationCrownOfSecret();
            var tmpArray = new byte[5];
            combination.MatrixToCombinationCrownOfSecret(matrix, bet, gratisGame, numberOfLines, ref tmpArray, true);
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var n = combination.LinesInformation.Length;
            var winLineList = new List<WinLineV3>();
            int[,] matrix;
            int[] tmpUpperRow;
            int[] tmpBottomRow;
            var exp = new List<WildExpandV3>();
            bool isHandFromBonus = combination.AdditionalInformation == 1;

            if (isHandFromBonus)
            {
                matrix = new int[1, 1];
                tmpUpperRow = new int[1];
                tmpBottomRow = new int[1];

                matrix[0, 0] = combination.Matrix[0, 0];
                tmpUpperRow[0] = (int)SoftwareRng.Next(1, 9);
                tmpBottomRow[0] = (int)SoftwareRng.Next(1, 9);
            }
            else
            {
                matrix = new int[5, 3];
                tmpUpperRow = new int[5];
                tmpBottomRow = new int[5];

                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        matrix[i, j] = combination.Matrix[i, j];
                    }

                    tmpUpperRow[i] = (combination.Matrix[i, 0] + 5) % 8 + 1;
                    tmpBottomRow[i] = (combination.Matrix[i, 2] + 5) % 8 + 1;
                }

                for (var i = 0; i < 5; i++)
                {
                    if (combination.PositionFor2[i] < 15)
                    {
                        var wld = new WildExpandV3
                        {
                            type = "expand",
                            origin = new CoordinateV3
                            { reel = combination.PositionFor2[i] % 5, row = combination.PositionFor2[i] / 5 }
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
            }


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

                WinSymbolV3[] winSymb;

                if (isHandFromBonus)
                {
                    winSymb = new WinSymbolV3[1];
                    winSymb[0] = new WinSymbolV3
                    {
                        reel = 0,
                        row = 0,
                        id = combination.Matrix[0, 0]
                    };
                }
                else
                {
                    var m = positions.Count;
                    winSymb = new WinSymbolV3[m];
                    for (var j = 0; j < m; j++)
                    {
                        winSymb[j] = new WinSymbolV3 { reel = positions[j] % 5, row = positions[j] / 5 };
                        winSymb[j].id = matrix[winSymb[j].reel, winSymb[j].row];
                        if (matrix[winSymb[j].reel, 0] * matrix[winSymb[j].reel, 1] * matrix[winSymb[j].reel, 2] == 0)
                        {
                            winSymb[j].id = 0;
                        }
                    }
                }

                wl.symbols = winSymb;
                winLineList.Add(wl);
            }


            var slotData = new SlotDataResV3
            {
                win = combination.TotalWin,
                symbols = matrix,
                extra = new
                {
                    upperRow = tmpUpperRow,
                    bottomRow = tmpBottomRow,
                    wildExpand = exp,
                    bonusData = (combination.GratisGame || isHandFromBonus) ? BonusDataCrownOfSecret.FromByteArray(combination.AdditionalArray) : null,
                },
                wins = winLineList.ToArray(),
                gratisGame = combination.GratisGame
            };

            return slotData;
        }
    }
}
