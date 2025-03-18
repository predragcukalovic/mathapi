using MathBaseProject.StructuresV3;
using MathForGames.GameTurboHot40;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.Game40MegaFlames
{
    public class Matrix40MegaFlames : MatrixTurboHot40
    {
        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLines40MegaFlames =
        {
            {0, 24, 80, 320, 2000},
            {0, 0, 32, 80, 200},
            {0, 0, 32, 80, 200},
            {0, 0, 16, 32, 80},
            {0, 0, 16, 32, 80},
            {0, 0, 8, 16, 40},
            {0, 0, 8, 16, 40},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static readonly int[] WinForScatter40MegaFlames = { 0, 0, 2, 8, 50 };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLine40MegaFlames).CalculateLineWin(WinForLines40MegaFlames, null, -1, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 6, 6, 6, 6, 7, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 0, 0, 0, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 4, 4, 4, 4, 5, 5, 5, 5, 7, 2, 2, 2, 2 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 7, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 0, 0, 0, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 4, 4, 4, 4, 5, 5, 5, 5, 7, 2, 2, 2, 2 };
            fakeReels[2] = new[] { 6, 6, 6, 6, 7, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 0, 0, 0, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 4, 4, 4, 4, 5, 5, 5, 5, 7, 2, 2, 2, 2 };
            fakeReels[3] = new[] { 6, 6, 6, 6, 7, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 0, 0, 0, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 4, 4, 4, 4, 5, 5, 5, 5, 7, 2, 2, 2, 2 };
            fakeReels[4] = new[] { 6, 6, 6, 6, 7, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 0, 0, 0, 0, 7, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 5, 5, 5, 5, 7, 4, 4, 4, 4, 5, 5, 5, 5, 7, 2, 2, 2, 2 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 7)
            {
                return WinForScatter40MegaFlames;
            }

            var coefficients = new int[5];

            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLines40MegaFlames[id, i];
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

            symbols[7] = new HelpSymbolConfigV3<object>
            {
                id = 7,
                features = new[] { HelpSymbolFeatureV3.Scatter },
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(7)
            };

            for (var i = 0; i < 7; i++)
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
                    pos[j] = UnicornGlobalData.GameLine40MegaFlames[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}
