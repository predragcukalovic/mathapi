using GameMegaHot;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

using System.Collections.Generic;


namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameMegaHot20Conversion
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
                tmpUpperRow[i] = matrix[i, 0] < 2 || matrix[i, 0] == 7 ? (int)SoftwareRng.Next(2, 7) : matrix[i, 0];
                tmpBottomRow[i] = matrix[i, 2] < 2 || matrix[i, 2] == 7 ? (int)SoftwareRng.Next(2, 7) : matrix[i, 2];
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
                    multiplicator = combination.WinFor2
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        public static Combination GetNonWinningCombination(int bet, int numberOfLines)
        {
            var matrixArray = new[,] { { 4, 4, 4 }, { 4, 4, 4 }, { 0, 1, 6 }, { 5, 1, 0 }, { 0, 2, 3 } };
            var matrix = new MatrixMegaHot10();
            matrix.FromMatrixArray(matrixArray);
            var combination = new CombinationMegaHot();
            combination.MatrixToCombinationMegaHot10(matrix, bet, numberOfLines);
            return combination;
        }

        #region V3 structs

        public static int[] PlayLines = { 20 };

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 3, 3, 3, 0, 1, 1, 1, 1, 1, 0, 2, 0, 5, 5, 2, 2, 2, 2, 2, 5, 0, 4, 4, 4, 4, 4, 0, 3, 3 };
            fakeReels[1] = new[] { 2, 2, 2, 0, 1, 1, 1, 1, 1, 0, 2, 5, 0, 4, 4, 4, 4, 4, 4, 0, 3, 3, 3, 3, 3, 5, 5, 2, 2 };
            fakeReels[2] = new[] { 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 5, 0, 4, 4, 4, 4, 4, 2, 3, 3, 3, 3, 3, 3, 0, 5, 1, 1, 1 };
            fakeReels[3] = new[] { 0, 5, 2, 2, 2, 2, 2, 5, 5, 4, 4, 4, 4, 4, 2, 5, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 0, 5, 5 };
            fakeReels[4] = new[] { 0, 3, 3, 3, 3, 5, 0, 2, 5, 2, 2, 2, 2, 5, 2, 4, 4, 4, 4, 0, 5, 1, 1, 1, 1, 5, 0, 5, 5 };
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
                coefficients[i] = MatrixMegaHot10.WinForLinesMegaHot[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.48,
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
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
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
