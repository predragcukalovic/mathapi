using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;


namespace MathForUnicornGames.GameWildParadice
{
    public class MatrixWildParadice : Matrix
    {
        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesWildParadice =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 100, 500, 2500},
            {0, 0, 25, 100, 500},
            {0, 0, 25, 100, 500},
            {0, 0, 20, 50, 250},
            {0, 0, 20, 50, 250},
            {0, 0, 20, 50, 250},
            {0, 0, 10, 25, 100},
            {0, 0, 10, 25, 100},
            {0, 0, 10, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije. Wild symbol za igru je  postavljen kao 0.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLine).CalculateLineWin(WinForLinesWildParadice, null, 0, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 9, 3, 8, 8, 9, 3, 9, 9, 1, 8, 8, 6, 6, 8, 8, 4, 4, 1, 9, 9, 7, 7, 5, 5, 9, 9, 8, 8, 2, 6, 6, 7, 7, 4, 9, 6, 8, 8, 8, 2, 2, 7, 2 };
            fakeReels[1] = new[] { 4, 1, 6, 6, 3, 3, 9, 9, 9, 2, 8, 0, 8, 8, 5, 5, 1, 6, 6, 2, 7, 6, 7, 7, 6, 5, 5, 6, 4, 7, 7, 0, 7, 7, 2, 7, 7, 4, 0, 4, 4, 8, 6, 6 };
            fakeReels[2] = new[] { 5, 5, 8, 8, 0, 8, 8, 3, 8, 2, 7, 7, 2, 2, 9, 9, 9, 2, 4, 4, 2, 6, 6, 3, 3, 8, 3, 8, 8, 1, 7, 7, 2, 9, 9, 9, 0, 9, 9, 3, 3, 2, 3 };
            fakeReels[3] = new[] { 7, 7, 6, 6, 2, 6, 2, 9, 9, 9, 3, 3, 7, 3, 7, 7, 7, 2, 2, 9, 9, 9, 1, 8, 8, 3, 8, 3, 6, 6, 5, 5, 4, 1, 4, 2, 9, 9, 8, 8, 0, 8 };
            fakeReels[4] = new[] { 6, 1, 7, 7, 5, 5, 2, 2, 6, 6, 7, 7, 7, 2, 4, 4, 1, 8, 3, 4, 3, 3, 8, 3, 8, 8, 9, 9, 1, 4, 4, 3, 3, 8, 3, 9, 9, 2, 4, 2, 7 };

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
                coefficients[i] = WinForLinesWildParadice[id, i];
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
        /// Trenutno implementacija za Wild symbol(0) dodeljuje Regular
        /// 10 je broj simbola u ovoj igri, vrednosti simbola 1-10
        /// </summary>
        /// <returns></returns>
        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[10];
            for (var i = 0; i < 10; i++)
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
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = UnicornGlobalData.GameLine[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
    }
}

