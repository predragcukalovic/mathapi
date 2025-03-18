using MathBaseProject.StructuresV3;
using MathForGames.GameTurboHot40;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameWinterFruits
{
    public class MatrixWinterFruits : MatrixTurboHot40
    {
        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLinesWinterFruits =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 40, 80, 320},
            {0, 0, 40, 80, 320},
            {0, 0, 20, 40, 120},
            {0, 0, 20, 40, 120},
            {0, 0, 10, 20, 40},
            {0, 0, 10, 20, 40},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForWildWinterFruits = { 0, 0, 80, 200, 1000 };
        public static readonly int[] WinForScatterWinterFruits = { 0, 0, 4, 20, 200 };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            var l = GetLine(lineNumber, UnicornGlobalData.GameLineWinterFruits);
            for (var i = 0; i < 5; i++)
            {
                if (l.GetElement(i) == 1)
                {
                    l.SetElement(i, 0);
                }
            }
            return l.CalculateLineWin(WinForLinesWinterFruits, WinForWildWinterFruits, 0, 1);
        }

        public int GetWinningElementForLine(int lineNum, int lineWin)
        {
            var l = GetLine(lineNum, UnicornGlobalData.GameLineWinterFruits);
            var wildCount1 = 0;
            for (var i = 0; i < 5; i++)
            {
                if (l.GetElement(i) == 1)
                {
                    wildCount1++;
                    l.SetElement(i, 0);
                }
            }
            var winElem = l.GetWinningElement(0, lineWin, WinForWildWinterFruits);
            return winElem == 0 ? (wildCount1 > 0 ? 1 : 0) : winElem;
        }

        public byte[] GetPositions(int line, int element)
        {
            var l = GetLine(line, UnicornGlobalData.GameLineWinterFruits);
            for (var i = 0; i < 5; i++)
            {
                if (l.GetElement(i) == 1)
                {
                    l.SetElement(i, 0);
                }
            }
            return l.GetLinesPositions(UnicornGlobalData.GameLineWinterFruits, line, 0, element == 1 ? 0 : element);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 7, 7, 7, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 0, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 7, 7, 7, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 0, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[2] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 7, 7, 7, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 0, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[3] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 7, 7, 7, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 0, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[4] = new[] { 6, 6, 6, 6, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 7, 7, 7, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 0, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2 };
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
                return WinForScatterWinterFruits;
            }
            if (id == 1)
            {
                return WinForWildWinterFruits;
            }

            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesWinterFruits[id, i];
            }
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

        /// <summary>
        /// Returns symbols configuration.
        /// </summary>
        /// <returns></returns>
        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];

            symbols[0] = new HelpSymbolConfigV3<object>
            {
                id = 0,
                features = new[] { HelpSymbolFeatureV3.Scatter },
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(0)
            };

            for (var i = 1; i < 8; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
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
                    pos[j] = UnicornGlobalData.GameLineWinterFruits[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}
