using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameJuicyHot;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameTwinklingHotConversion
    {
        public static int[] PlayLines = { 5 };

        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 7, 7, 7 }, { 6, 6, 6 }, { 3, 3, 3 }, { 4, 4, 4 }, { 5, 5, 5 } };
            var matrix = new MatrixJuicyHot();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 7, 7, 4, 4, 3, 3, 5, 3, 5, 5, 5, 6, 1, 6, 6, 6, 2, 1, 2, 2, 0, 4, 4, 4, 2, 2, 3, 3, 5, 5, 5 };
            fakeReels[1] = new[] { 7, 7, 7, 7, 0, 6, 6, 6, 6, 5, 5, 5, 2, 5, 2, 2, 4, 1, 4, 4, 4, 3, 3, 1, 3, 0, 3, 3, 6, 6, 2, 2 };
            fakeReels[2] = new[] { 7, 7, 7, 0, 5, 5, 5, 1, 5, 3, 3, 6, 3, 6, 6, 6, 2, 2, 7, 2, 4, 4, 4, 1, 4, 1, 2, 2, 1, 3, 3, 0 };
            fakeReels[3] = new[] { 7, 7, 7, 6, 6, 3, 3, 1, 5, 5, 5, 5, 1, 5, 6, 6, 6, 0, 2, 2, 4, 4, 4, 7, 4, 6, 6, 5, 5, 2, 2, 3, 3 };
            fakeReels[4] = new[] { 7, 7, 7, 2, 2, 1, 6, 6, 4, 4, 4, 3, 3, 5, 0, 5, 5, 5, 0, 6, 6, 6, 4, 1, 0, 2, 2, 1, 7, 7, 7, 5, 5, 5, 2, 2, 2 };

            return fakeReels;
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
                tmpUpperRow[i] = (int)SoftwareRng.Next(8);
                tmpBottomRow[i] = (int)SoftwareRng.Next(8);
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
                    bottomRow = tmpBottomRow
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 2)
            {
                return LineWinsForGames.WinForScattersJuicyHot;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesJuicyHot[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.66,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
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
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
