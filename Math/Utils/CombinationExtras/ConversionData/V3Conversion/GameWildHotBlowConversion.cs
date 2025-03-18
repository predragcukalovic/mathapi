using CombinationExtras.ConversionData.V3Conversion.OtherStructuresV3;
using GameWildHot40Blow;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;


namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameWildHotBlowConversion
    {
        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 3, 3, 3, 3, 3, 3 }, { 7, 7, 7, 7, 7, 7 }, { 6, 6, 6, 6, 6, 6 }, { 5, 5, 5, 5, 5, 5 }, { 4, 4, 4, 4, 4, 4 } };
            var matrix = new MatrixWildHot40Blow();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationWildHot40Blow();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
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

        #region V3 structs

        public static int[] PlayLines = { 40 };

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 6, 6, 6, 6, 1, 1, 1, 1, 2, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 0, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 2, 3, 3, 3, 3, 6, 6, 6, 6, 1, 1, 1, 1 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 0, 6, 6, 6, 6, 5, 5, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 4, 4, 4, 4 };
            fakeReels[2] = new[] { 4, 4, 4, 4, 7, 7, 7, 7, 2, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 2, 4, 4, 4, 4, 0, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 2, 1, 1, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 1, 1, 1, 1 };
            fakeReels[3] = new[] { 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 0, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 5, 5, 5, 5, 2, 6, 6, 6, 6, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 6, 6, 6, 6, 3, 3, 3, 3, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 6, 6, 6, 6, 2 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 2, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 0, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 2, 4, 4, 4, 4, 6, 6, 6, 6, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 0, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesTurboHot40[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.27,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[7];
            for (var i = 0; i < 7; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[40];
            for (var i = 0; i < 40; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineTurbo[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
