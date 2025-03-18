using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameFruits;
using RNGUtils.RandomData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameFruitsAndStarsConversion
    {
        public static int[] PlayLines = { 20 };

        /// <summary>
        /// Izmenjeno da daje id-jeve za jedan manji, za ING.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[5, 3];
            var tmpUpperRow = new int[5];
            var tmpBottomRow = new int[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    matrix[i, j] = combination.Matrix[i, j] - 1;
                }
                tmpUpperRow[i] = (int)SoftwareRng.Next(2, 8);
                tmpBottomRow[i] = (int)SoftwareRng.Next(2, 8);
            }
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            for (var i = 0; i < n; i++)
            {
                winLine[i] = new WinLineV3
                {
                    lineId = combination.LinesInformation[i].Id,
                    soundId = combination.LinesInformation[i].WinningElement - 1,
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
        /// Izmenjeno da daje id-jeve za jedan manji, za ING.
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="numberOfLines"></param>
        /// <param name="gratisGamesLeft"></param>
        /// <returns></returns>
        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 7, 7, 7 }, { 3, 3, 3 }, { 6, 6, 6 }, { 4, 4, 4 }, { 5, 5, 5 } };
            var matrix = new MatrixFruits();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet);
            return combination;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja, Izmenjeno da daje id-jeve za jedan manji, za ING.
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 5, 5, 5, 1, 1, 1, 7, 7, 7, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 7, 7, 7, 3, 3, 3, 4, 4, 4, 5, 5, 5, 2, 2, 2, 1, 1, 1, 7, 7, 7, 0, 6, 6, 6, 5, 5, 5, 2, 2, 2, 0, 7, 7 };
            fakeReels[1] = new[] { 5, 7, 7, 7, 3, 3, 3, 4, 4, 4, 1, 1, 1, 6, 6, 6, 3, 3, 3, 7, 7, 7, 6, 6, 6, 7, 7, 7, 5, 5, 5, 6, 6, 6, 1, 1, 1, 5, 5, 5, 7, 7, 7, 4, 4, 4, 2, 2, 2, 7, 7, 7, 0, 4, 4, 4, 2, 2, 2, 7, 7, 7, 0, 3, 3, 3, 5, 5 };
            fakeReels[2] = new[] { 4, 4, 6, 6, 6, 5, 5, 5, 1, 1, 1, 3, 3, 3, 4, 4, 4, 6, 6, 6, 7, 7, 7, 4, 4, 4, 3, 3, 3, 1, 1, 1, 6, 6, 6, 4, 4, 4, 7, 7, 7, 3, 3, 3, 4, 4, 4, 5, 5, 5, 2, 2, 2, 7, 7, 7, 0, 2, 2, 2, 3, 3, 3, 5, 5, 5, 4, 4 };
            fakeReels[3] = new[] { 7, 5, 5, 5, 0, 4, 4, 4, 7, 7, 7, 3, 3, 3, 4, 4, 4, 2, 2, 2, 3, 3, 3, 7, 7, 7, 1, 1, 1, 5, 5, 5, 6, 6, 6, 4, 4, 4, 2, 2, 2, 6, 6, 6, 7, 7, 7, 5, 5, 5, 1, 1, 1, 4, 4, 4, 6, 6, 6, 3, 3, 3, 7, 7 };
            fakeReels[4] = new[] { 7, 3, 3, 3, 0, 4, 4, 4, 6, 6, 6, 7, 7, 7, 1, 1, 1, 2, 2, 2, 5, 5, 5, 6, 6, 6, 7, 7, 7, 5, 5, 5, 4, 4, 4, 3, 3, 3, 7, 7, 7, 4, 4, 4, 3, 3, 3, 5, 5, 5, 1, 1, 1, 7, 7, 7, 6, 6, 6, 5, 5, 5, 2, 2, 2, 7, 7 };

            return fakeReels;
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
                return LineWinsForGames.WinForWildsFruits;
            }
            if (id == 1)
            {
                return LineWinsForGames.WinForScattersFruits;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesFruits[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.8,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 1; i < 9; i++)
            {
                symbols[i - 1] = new HelpSymbolConfigV3<object>
                {
                    id = i - 1,
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
