using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;
using System;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    #region Data structure

    public class TurboHot
    {
        public int[] symbols { get; set; }
        public int[] upperRow { get; set; }
        public int[] bottomRow { get; set; }
        public int totalSum { get; set; }
        public byte noWinLines { get; set; }
        public int numberOfFreeSpins { get; set; }
        public bool isGratis { get; set; }
        public LineInfoJson[] winStruct { get; set; }
    }

    public class TurboHotFree
    {
        public int[] symbols { get; set; }
        public int[] upperRow { get; set; }
        public int[] bottomRow { get; set; }
        public int totalSum { get; set; }
        public byte noWinLines { get; set; }
        public int numberOfFreeSpins { get; set; }
        public bool isGratis { get; set; }
        public int numOfBonus { get; set; }
        public int bonus { get; set; }
        public LineInfoJson[] winStruct { get; set; }
    }

    #endregion Data structure

    public class GameTurboHot40Conversion
    {
        public static int[] PlayLines = { 40 };

        /// <summary>
        /// Pretvara kombinaciju u JSON objekat.
        /// </summary>
        /// <param name="combination"></param>
        /// <returns></returns>
        public static object ToJsonObject(ICombination combination)
        {
            var tmpMatrixArray = new byte[20];
            var tmpUpperRow = new byte[5];
            var tmpBottomRow = new byte[5];
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    tmpMatrixArray[j * 5 + i] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 5];
                tmpBottomRow[i] = combination.Matrix[i, 4];
            }

            var obj = new TurboHot
            {
                symbols = Array.ConvertAll(tmpMatrixArray, c => (int)c),
                upperRow = Array.ConvertAll(tmpUpperRow, c => (int)c),
                bottomRow = Array.ConvertAll(tmpBottomRow, c => (int)c),
                totalSum = combination.TotalWin,
                noWinLines = combination.NumberOfWinningLines,
                numberOfFreeSpins = combination.NumberOfGratisGames,
                isGratis = false,
                winStruct = CommonV3Conversion.ToLineInfoJson(combination.LinesInformation)
            };
            return obj;
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

        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 5, 5, 5, 5, 5, 5 }, { 4, 4, 4, 4, 4, 4 }, { 7, 7, 7, 7, 7, 7 }, { 3, 3, 3, 3, 3, 3 }, { 6, 6, 6, 6, 6, 6 } };
            var matrix = new MatrixTurboHot40();
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
            fakeReels[0] = new[] { 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 0, 0, 0, 6, 6, 6, 6, 1, 1, 1, 1, 2, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 0, 0, 0, 0, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 2, 3, 3, 3, 3, 6, 6, 6, 6, 1, 1, 1, 1 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 0, 0, 0, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 0, 0, 0, 0, 6, 6, 6, 6, 5, 5, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 7, 7, 7, 7, 1, 1, 1, 1, 5, 5, 5, 5, 2, 4, 4, 4, 4 };
            fakeReels[2] = new[] { 4, 4, 4, 4, 7, 7, 7, 7, 2, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 2, 4, 4, 4, 4, 0, 0, 0, 0, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 0, 0, 0, 0, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 2, 1, 1, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 1, 1, 1, 1 };
            fakeReels[3] = new[] { 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 0, 0, 0, 0, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 5, 5, 5, 5, 2, 6, 6, 6, 6, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0, 0, 6, 6, 6, 6, 3, 3, 3, 3, 7, 7, 7, 7, 1, 1, 1, 1, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 6, 6, 6, 6, 2 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 2, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 2, 4, 4, 4, 4, 6, 6, 6, 6, 0, 0, 0, 0, 5, 5, 5, 5, 3, 3, 3, 3, 2, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 0, 0, 0, 0, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return LineWinsForGames.WinForWildsTurboHot40;
            }
            if (id == 2)
            {
                return LineWinsForGames.WinForScatterTurboHot40;
            }
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
                rtp = (decimal?)96.58,
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
