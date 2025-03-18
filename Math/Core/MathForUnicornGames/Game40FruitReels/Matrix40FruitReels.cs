using MathBaseProject.StructuresV3;
using MathForGames.GameTurboHot40;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.Game40FruitReels
{
    public class Matrix40FruitReels : MatrixTurboHot40
    {
        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLines40FruitReels =
        {
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 200, 500},
            {0, 0, 50, 200, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
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

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineWinterFruits).CalculateLineWin(WinForLines40FruitReels, null, -1, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];

            fakeReels[0] = new[] { 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 3, 3, 2, 2, 2, 0, 0, 0, 0, 5, 5, 5, 5, 6, 5, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 4, 6, 4, 4, 4, 2, 2, 5, 5, 5, 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 3, 3, 3, 5, 5, 5, 5 };
            fakeReels[1] = new[] { 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 3, 3, 2, 2, 2, 0, 0, 0, 0, 5, 5, 5, 5, 6, 5, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 4, 6, 4, 4, 4, 2, 2, 5, 5, 5, 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 3, 3, 3, 5, 5, 5, 5 };
            fakeReels[2] = new[] { 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 3, 3, 2, 2, 2, 0, 0, 0, 0, 5, 5, 5, 5, 6, 5, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 4, 6, 4, 4, 4, 2, 2, 5, 5, 5, 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 3, 3, 3, 5, 5, 5, 5 };
            fakeReels[3] = new[] { 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 3, 3, 2, 2, 2, 0, 0, 0, 0, 5, 5, 5, 5, 6, 5, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 4, 6, 4, 4, 4, 2, 2, 5, 5, 5, 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 3, 3, 3, 5, 5, 5, 5 };
            fakeReels[4] = new[] { 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 3, 3, 2, 2, 2, 0, 0, 0, 0, 5, 5, 5, 5, 6, 5, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 4, 6, 4, 4, 4, 2, 2, 5, 5, 5, 4, 4, 4, 0, 0, 5, 5, 5, 6, 6, 3, 3, 3, 5, 5, 5, 5 };
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
                coefficients[i] = WinForLines40FruitReels[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[7];

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
                    pos[j] = UnicornGlobalData.GameLineWinterFruits[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}
