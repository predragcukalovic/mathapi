using GameJokerQueen;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System.Collections.Generic;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameJokerQueenConversion
    {
        public static int[] PlayLines = { 5 };
        public static Combination3 GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 1, 1, 2, 3, 1 }, { 4, 4, 5, 6, 6 }, { 7, 7, 7, 3, 3 } };
            var matrix = new MatrixJokerQueen();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination3();

            var additionalArray = new byte[5];

            combination.MatrixToCombination(matrix, numberOfLines, bet, gratisGamesLeft > 0,
                (byte)0, ref additionalArray
                );
            return combination;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination)
        {
            var matrix = new int[3, 3];
            var tmpUpperRow = new int[3];
            var tmpBottomRow = new int[3];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    matrix[i, j - 1] = combination.Matrix[i, j];
                }
                tmpUpperRow[i] = combination.Matrix[i, 0];
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
                while (index < 3 && combination.LinesInformation[i].WinningPosition[index] != 255)
                {
                    positions.Add(combination.LinesInformation[i].WinningPosition[index++]);
                }
                var m = positions.Count;
                var winSymbol = new WinSymbolV3[m];
                for (var j = 0; j < m; j++)
                {
                    winSymbol[j] = new WinSymbolV3 { reel = positions[j] % 3, row = positions[j] / 3 };
                    winSymbol[j].id = matrix[winSymbol[j].reel, winSymbol[j].row];
                }
                winLine[i].symbols = winSymbol;
            }

            int reelToBeRespined = -1;
            if (combination.AdditionalArray != null)
            {
                var sum = combination.AdditionalArray[0] + combination.AdditionalArray[1];
                if (sum > 0 && sum < 4)
                {
                    reelToBeRespined = 3 - sum;
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
                    multiplier = combination.AdditionalInformation,
                    respinReel = reelToBeRespined
                },
                wins = winLine,
                gratisGame = combination.GratisGame
            };

            return slotData;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[3][];
            fakeReels[0] = new[] { 2, 2, 2, 2, 7, 7, 7, 7, 8, 8, 8, 8, 1, 1, 1, 1, 1, 5, 5, 5, 5, 8, 3, 8, 4, 4, 4, 4, 3, 3, 3, 1, 1, 1, 6, 6, 6, 6, 2, 7, 7, 0, 0, 0, 0, 0 };
            fakeReels[1] = new[] { 2, 2, 2, 2, 2, 8, 3, 8, 4, 4, 4, 4, 4, 3, 3, 3, 6, 6, 6, 6, 6, 1, 7, 7, 7, 1, 8, 8, 8, 5, 5, 5, 5, 5, 1, 1, 1, 2, 0, 0, 0, 0, 0, 0 };
            fakeReels[2] = new[] { 2, 2, 2, 2, 7, 7, 7, 7, 1, 1, 1, 8, 8, 2, 8, 8, 1, 5, 5, 5, 5, 2, 8, 8, 3, 4, 4, 4, 4, 3, 3, 3, 6, 6, 6, 6, 7, 7, 0, 0, 0, 0, 0, 0 };

            return fakeReels;
        }


        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[3];
            coefficients[0] = 0;
            coefficients[1] = 0;
            coefficients[2] = MatrixJokerQueen.WinForLinesJokerQueen[id];
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[9];
            for (var i = 0; i < 9; i++)
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
                var pos = new int[3];
                for (var j = 0; j < 3; j++)
                {
                    pos[j] = GlobalData.GameLineVegasHot[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
