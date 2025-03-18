using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathCombination.CombinationData;
using MathForGames.BasicGameData;
using System;
using System.Collections.Generic;

namespace GameVeryHot5Extreme
{
    public class MatrixVeryHot5Extreme : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesVeryHotExtreme =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 10, 50, 200, 3000},
            {0, 0, 40, 100, 500},
            {0, 0, 40, 100, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildVeryHotExtreme = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterVeryHotExtreme = { 0, 0, 3, 20, 100 };
        public static int[] PlayLines = { 5 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            var m = 1;
            var l = GetLine(lineNumber, GlobalData.GameLineExtra);
            if (l.GetElement(2) == 1)
            {
                l.SetElement(2, 0);
                m = 2;
            }
            return l.CalculateLineWin(WinForLinesVeryHotExtreme, WinForWildVeryHotExtreme, 0, m);
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
                    if (el < 15 && GetElement(el % 5, el / 5) <= 1)
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

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 7, 7, 7, 2, 4, 4, 3, 5, 5, 8, 8, 8, 2, 3, 3, 6, 6, 6, 2, 7, 7, 3, 6, 6, 4, 9, 9, 8, 8, 8, 3, 3, 10, 2, 9, 9, 6, 6, 5, 5, 4, 4, 3, 5, 5, 5, 10, 4, 4, 8, 8, 6, 3, 10, 2, 7, 7, 9, 9, 9 };
            fakeReels[1] = new[] { 4, 4, 2, 9, 9, 9, 10, 8, 8, 5, 5, 5, 0, 3, 6, 6, 8, 8, 7, 7, 7, 0, 4, 4, 5, 5, 5, 2, 10, 3, 3, 6, 6, 6, 8, 8, 2, 4, 4, 9, 9, 6, 6, 0, 5, 5, 2, 6, 6, 3, 3, 8, 8, 7, 7, 10, 9, 9, 7, 7 };
            fakeReels[2] = new[] { 9, 9, 2, 4, 4, 5, 5, 5, 0, 6, 6, 10, 8, 8, 2, 1, 3, 3, 9, 9, 9, 8, 7, 7, 0, 6, 6, 6, 1, 7, 7, 4, 4, 9, 9, 10, 2, 5, 5, 5, 8, 8, 8, 10, 6, 6, 0, 4, 4, 1, 3, 3, 8, 8, 2, 7, 7, 6, 6, 1, 5, 5, 5, 4, 4, 7, 7, 2, 6, 6, 5, 7, 7, 10, 9, 9, 9, 3, 8, 8 };
            fakeReels[3] = new[] { 4, 4, 7, 7, 7, 2, 6, 6, 9, 9, 10, 8, 8, 8, 0, 5, 5, 4, 4, 7, 7, 7, 9, 9, 10, 3, 3, 6, 6, 6, 0, 4, 4, 8, 8, 8, 5, 5, 10, 7, 7, 5, 0, 6, 6, 6, 8, 8, 2, 9, 9, 5, 5, 4, 4, 2, 3, 9, 9, 2 };
            fakeReels[4] = new[] { 6, 6, 8, 8, 8, 2, 9, 9, 9, 2, 7, 7, 7, 10, 5, 5, 7, 9, 6, 3, 3, 4, 8, 10, 7, 7, 8, 8, 8, 4, 4, 5, 5, 5, 6, 6, 2, 10, 9, 9, 4, 4, 8, 8, 9, 9, 4, 4, 6, 6, 3, 3, 2, 7, 7, 6, 2, 5, 5, 10 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id < 2)
            {
                return WinForWildVeryHotExtreme;
            }
            if (id == 10)
            {
                return WinForScatterVeryHotExtreme;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesVeryHotExtreme[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.31,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[11];
            for (var i = 0; i < 11; i++)
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
