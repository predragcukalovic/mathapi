using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using MathForGames.GameSpellbook;
using System.Collections.Generic;
using System.Linq;

namespace CombinationExtras.ConversionData.V3Conversion
{
    public class GameSpellbookConversion
    {
        public static int[] PlayLines = { 10 };

        public static Combination GetNonWinningCombination(int bet, int numberOfLines, int gratisGamesLeft)
        {
            var matrixArray = new[,] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 }, { 8, 7, 6 }, { 5, 4, 9 } };
            var matrix = new MatrixSpellbook();
            matrix.FromMatrixArray(matrixArray);
            var combination = new Combination();
            combination.MatrixToCombination(matrix, numberOfLines, bet, false, 0);
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
            fakeReels[0] = new[] { 9, 8, 7, 2, 9, 7, 8, 0, 6, 4, 9, 8, 6, 2, 8, 7, 6, 5, 7, 8, 3, 9, 5, 7, 6, 8, 5, 7, 9, 0, 5, 8, 9, 7, 1 };
            fakeReels[1] = new[] { 9, 8, 7, 0, 5, 8, 7, 2, 6, 5, 8, 6, 9, 8, 4, 7, 5, 6, 9, 2, 8, 6, 5, 7, 4, 9, 5, 1, 8, 9, 5, 7, 3, 0, 4 };
            fakeReels[2] = new[] { 9, 8, 5, 0, 9, 6, 2, 5, 7, 4, 8, 9, 4, 6, 5, 4, 7, 5, 1, 6, 9, 2, 7, 6, 8, 3, 7, 2, 6, 8, 3, 5, 9, 2, 0 };
            fakeReels[3] = new[] { 9, 8, 4, 6, 9, 1, 0, 7, 5, 2, 8, 7, 3, 8, 6, 4, 5, 6, 1, 7, 3, 6, 2, 9, 7, 3, 9, 6, 8, 2, 5, 9, 4, 0, 1 };
            fakeReels[4] = new[] { 9, 8, 4, 5, 6, 4, 9, 5, 8, 1, 5, 8, 3, 7, 5, 4, 9, 8, 0, 6, 8, 7, 5, 4, 7, 9, 2, 5, 3, 7, 6, 1, 9, 0, 2 };

            return fakeReels;
        }

        public static SlotDataResV3 ToSlotDataResV3(ICombination combination, bool isCurrentGameGratis)
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
                tmpUpperRow[i] = (combination.Matrix[i, 0] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 0] + 1) % 10 : (combination.Matrix[i, 0] + 5) % 10;
                tmpBottomRow[i] = (combination.Matrix[i, 2] + 5) % 10 == combination.Matrix[i, 1] ? (combination.Matrix[i, 2] + 1) % 10 : (combination.Matrix[i, 2] + 5) % 10;
            }
            var n = combination.LinesInformation.Length;
            var winLine = new WinLineV3[n];
            var winLineList = new List<WinLineV3>();
            var winLineListExtra = new List<WinLineV3>();
            var extraLines = false;
            long extraWin = 0;
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
                if (extraLines)
                {
                    winLineListExtra.Add(winLine[i]);
                    extraWin += winLine[i].win;
                }
                else
                {
                    if (winLine[i].win > 0)
                    {
                        winLineList.Add(winLine[i]);
                    }
                }
                if (winLine[i].lineId == 254)
                {
                    extraLines = true;
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
                    bonusSymbol = combination.AdditionalInformation == 0 ? null : (int?)combination.AdditionalInformation,
                    transformReels = isCurrentGameGratis ? combination.PositionFor2.Select(x => (int)x).ToArray() : new[] { 0, 0, 0, 0, 0 },
                    winsExtra = winLineListExtra.ToArray(),
                    winExtra = extraWin
                },
                wins = winLineList.ToArray(),
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
            if (id == 0)
            {
                return LineWinsForGames.WinForGratisSpellbook;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = LineWinsForGames.WinForLinesSpellbook[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.1,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];
            for (var i = 0; i < 10; i++)
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
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
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
