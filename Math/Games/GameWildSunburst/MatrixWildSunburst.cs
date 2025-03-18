using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameWildSunburst
{
    public class MatrixWildSunburst : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesWildSunburst =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 40, 100, 400},
            {0, 0, 15, 30, 100},
            {0, 0, 10, 20, 80},
            {0, 0, 10, 20, 80},
            {0, 0, 2, 10, 40},
            {0, 0, 2, 10, 40},
            {0, 0, 2, 10, 40},
            {0, 0, 2, 10, 40},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildWildSunburs = { 0, 0, 40, 100, 400 };
        public static readonly int[] WinForScatterWildSunburs = { 0, 0, 1, 2, 20 };
        public static int[] PlayLines = { 40 };
        public const int GRATIS_GAMES = 3;
        public static readonly double[] OverthrowProbabilities = { 0.008, 0.03, 0.18, 0.43, 0.212, 0.07, 0.048, 0.02, 0.0011, 0.0004, 0.0003, 0.00015, 0.00005 };

        #endregion

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 15</param>
        /// <param name="lines">Linije </param>
        /// <returns>vraća liniju pod datim brojem</returns>
        private LineWildSunburst GetLine(int lineNumber)
        {
            var line = new LineWildSunburst();
            for (var i = 0; i < 5; i++)
            {
                line.SetElement(i, GetElement(i, GlobalData.GameLineExtra[lineNumber - 1, i]));
            }
            return line;
        }

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber, bool gratisGame, out int winningElement, out byte[] winningPosition)
        {
            return GetLine(lineNumber).CalculateLineWin(WinForLinesWildSunburst, WinForWildWildSunburs, gratisGame, lineNumber, out winningElement, out winningPosition);
        }

        public static int GetOverthrowsNumber()
        {
            var sum = 0.0;
            var rnd = SoftwareRng.Next();
            for (var i = 0; i < OverthrowProbabilities.Length; i++)
            {
                sum += OverthrowProbabilities[i];
                if (rnd < sum)
                {
                    return i;
                }
            }
            return -1;
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
            fakeReels[0] = new[] { 2, 2, 3, 3, 3, 7, 6, 6, 9, 4, 1, 5, 5, 5, 0, 8, 8, 8, 1, 6, 6, 2, 0, 7, 7, 7, 9, 3, 3, 2, 6, 5, 0, 4, 4, 4, 1, 2, 2, 9, 8, 3, 0, 7, 7, 1, 1, 4, 9, 3, 3, 2, 1, 8, 8, 0 };
            fakeReels[1] = new[] { 1, 1, 7, 7, 7, 0, 2, 2, 9, 6, 6, 6, 4, 0, 5, 5, 4, 4, 8, 8, 8, 9, 1, 1, 3, 3, 3, 0, 2, 2, 4, 4, 4, 1, 6, 6, 9, 3, 8, 8, 0, 5, 5, 5, 9, 3, 2, 4, 4, 6, 6, 0, 5, 5, 5, 9 };
            fakeReels[2] = new[] { 7, 7, 0, 2, 2, 6, 8, 3, 3, 3, 9, 4, 4, 0, 5, 1, 1, 9, 2, 2, 6, 6, 6, 9, 5, 5, 5, 0, 3, 1, 1, 7, 7, 7, 0, 2, 2, 3, 8, 8, 8, 9, 4, 4, 4, 0, 3, 3, 1, 1, 6, 6, 9, 2, 2, 5, 5, 8 };
            fakeReels[3] = new[] { 3, 9, 8, 8, 8, 1, 1, 0, 5, 6, 6, 3, 2, 2, 9, 4, 4, 0, 7, 7, 7, 1, 1, 9, 6, 6, 6, 2, 2, 8, 3, 3, 0, 4, 4, 4, 1, 5, 5, 3, 7, 2, 9, 1, 8, 4, 0, 3, 3, 6, 6, 1, 7, 9, 5, 3, 7, 0 };
            fakeReels[4] = new[] { 4, 2, 8, 7, 0, 4, 4, 4, 9, 6, 6, 1, 1, 0, 3, 3, 3, 2, 8, 5, 9, 1, 1, 6, 3, 0, 2, 2, 7, 7, 7, 9, 5, 5, 5, 2, 0, 4, 8, 1, 1, 9, 6, 6, 2, 2, 0, 5, 2, 8, 4, 3, 3, 9, 2, 8, 8 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusus
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 2, 3, 6, 7, 7, 1, 1, 8, 4, 2, 7, 6, 8, 5, 5, 3, 4, 1, 8, 3, 6, 6, 1, 1, 5, 7, 3, 4, 5, 2, 2, 8, 6, 7 };
            fakeReels[1] = new[] { 3, 4, 6, 6, 3, 7, 2, 5, 5, 1, 1, 6, 8, 3, 7, 8, 3, 5, 6, 4, 7, 7, 7, 4, 8, 8, 1, 1, 5, 2, 2, 6, 3, 5, 1 };
            fakeReels[2] = new[] { 10, 10, 10 };
            fakeReels[3] = new[] { 2, 5, 5, 1, 3, 6, 4, 7, 3, 8, 4, 5, 1, 1, 6, 8, 2, 4, 7, 5, 3, 6, 1, 2, 8, 7, 1, 3, 3, 5, 5, 2, 8, 4, 4 };
            fakeReels[4] = new[] { 3, 3, 6, 2, 4, 5, 1, 8, 3, 2, 2, 8, 4, 7, 7, 1, 1, 5, 3, 7, 1, 3, 6, 6, 4, 8, 2, 2, 6, 1, 4, 5, 5 };
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
                return WinForWildWildSunburs;
            }
            if (id == 9)
            {
                return WinForScatterWildSunburs;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesWildSunburst[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.53,
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
                    features = new[] { i == 9 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular }
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
                    pos[j] = GlobalData.GameLineExtra[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
