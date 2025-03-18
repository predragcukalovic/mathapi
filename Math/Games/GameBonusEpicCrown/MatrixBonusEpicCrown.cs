using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System;
using System.Collections.Generic;

namespace GameBonusEpicCrown
{
    public class MatrixBonusEpicCrown : Matrix
    {
        public static readonly int[,] WinForLinesBonusEpicCrown =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 50, 200, 5000},
            {0, 0, 40, 120, 700},
            {0, 0, 40, 120, 700},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 10, 30, 150},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildBonusEpicCrown = { 0, 0, 0, 0, 0 };
        public static readonly int[] GratisGamesBonusEpicCrown = { 10, 15, 20 };
        public static int[] PlayLines = { 10 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesBonusEpicCrown, WinForWildBonusEpicCrown, 0, 1);
        }

        /// <summary>
        /// Ako su samo dobici sačinjeni od dva elementa i ne mora da se širi četvrti ril; nakon širenja vajldova se poziva.
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public byte[] FixExpand(IEnumerable<LineInfo> lineInfo, byte[] position2)
        {
            var shouldBeFixed = new[] { true, true, true };
            foreach (var info in lineInfo)
            {
                for (var i = 0; i < 5; i++)
                {
                    var el = info.WinningPosition[i];
                    if (el < 15 && GetElement(el % 5, el / 5) == 0)
                    {
                        shouldBeFixed[el % 5 - 1] = false;
                    }
                }
            }
            for (var i = 0; i < 5; i++)
            {
                if (position2[i] < 15)
                {
                    if (shouldBeFixed[position2[i] % 5 - 1])
                    {
                        position2[i] = 255;
                    }
                }
            }
            Array.Sort(position2);
            return position2;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 8, 1, 1, 1, 1, 3, 3, 6, 6, 7, 7, 7, 2, 2, 2, 9, 4, 4, 4, 1, 1, 6, 6, 6, 9, 5, 5, 5, 2, 2, 8, 8, 4, 4, 4, 9, 3, 3, 3, 5, 5, 7, 7 };
            fakeReels[1] = new[] { 8, 8, 9, 5, 5, 5, 3, 3, 3, 7, 7, 7, 9, 1, 1, 1, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 9, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[2] = new[] { 8, 8, 9, 5, 5, 5, 3, 3, 3, 7, 7, 7, 9, 1, 1, 1, 6, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 9, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[3] = new[] { 1, 1, 1, 9, 3, 3, 3, 0, 5, 5, 6, 6, 6, 0, 4, 4, 4, 2, 2, 9, 3, 3, 5, 5, 5, 7, 7, 0, 1, 1, 1, 8, 8, 8, 7, 7, 7, 9, 2, 2, 2, 2, 4, 4, 6, 6, 6, 8, 8 };
            fakeReels[4] = new[] { 9, 7, 7, 7, 1, 1, 1, 6, 6, 3, 3, 3, 9, 5, 5, 5, 4, 4, 8, 8, 8, 5, 5, 6, 6, 6, 9, 2, 2, 4, 4, 4, 1, 1, 1, 1, 8, 8, 2, 2, 2, 7, 7, 3, 3 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja, za gratis igre.
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 8, 1, 1, 1, 1, 3, 3, 6, 6, 7, 7, 7, 2, 2, 2, 4, 4, 4, 1, 1, 6, 6, 6, 5, 5, 5, 2, 2, 8, 8, 4, 4, 4, 3, 3, 3, 5, 5, 7, 7 };
            fakeReels[1] = new[] { 8, 8, 5, 5, 5, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[2] = new[] { 8, 8, 5, 5, 5, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[3] = new[] { 1, 1, 1, 3, 3, 3, 0, 5, 5, 6, 6, 6, 0, 4, 4, 4, 2, 2, 3, 3, 5, 5, 5, 7, 7, 0, 1, 1, 1, 8, 8, 8, 7, 7, 7, 2, 2, 2, 2, 4, 4, 6, 6, 6, 8, 8 };
            fakeReels[4] = new[] { 7, 7, 7, 1, 1, 1, 6, 6, 3, 3, 3, 5, 5, 5, 4, 4, 8, 8, 8, 5, 5, 6, 6, 6, 2, 2, 4, 4, 4, 1, 1, 1, 1, 8, 8, 2, 2, 2, 7, 7, 3, 3 };
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
                return WinForWildBonusEpicCrown;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBonusEpicCrown[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3(int numberOfLines = 40)
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.53,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3(numberOfLines)
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
                    features = new[] { i == 9 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular }
                };
            }

            return symbols;
        }

        private static HelpLineConfigV3[] GetHelpLineConfigV3(int numberOfLines = 40)
        {
            var lines = new HelpLineConfigV3[numberOfLines];
            for (var i = 0; i < numberOfLines; i++)
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
