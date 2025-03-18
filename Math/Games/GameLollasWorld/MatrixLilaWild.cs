using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace GameLollasWorld
{
    public class MatrixLilaWild : MatrixTurboHot40
    {
        #region Public properties

        public static int[] PlayLines = { 40 };

        public static readonly int[,] WinForLinesLilaWild =
        {
            {0, 0, 0, 0, 0},
            {0, 4, 60, 200, 1000},
            {0, 0, 40, 100, 300},
            {0, 0, 20, 80, 200},
            {0, 0, 20, 80, 200},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 8, 40, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildsLilaWild = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatterLilaWild = { 0, 0, 2, 20, 500 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLinesLilaWild, WinForWildsLilaWild, 0, 1);
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 9, 7, 7, 7, 7, 2, 2, 2, 2, 8, 8, 8, 8, 1, 1, 1, 1, 6, 6, 6, 6, 4, 4, 4, 4, 7, 7, 7, 7, 9, 6, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 5, 3, 3, 3, 3, 8, 8, 8, 8, 9, 7, 7, 7, 7, 6, 6, 6, 6, 4, 4, 4, 4, 6, 6, 6, 6, 9, 5, 5, 5, 5, 4, 4, 4, 4, 9, 7, 7, 7, 7, 5, 5, 5, 5, 9, 6, 6, 6, 6, 8, 8, 8, 8, 7, 7, 7, 7, 2, 2, 2, 2 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 0, 0, 0, 0, 8, 8, 8, 8, 3, 3, 3, 3, 9, 7, 7, 7, 7, 1, 1, 1, 1, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 8, 8, 8, 8, 6, 6, 6, 6, 2, 2, 2, 2, 8, 8, 8, 8, 1, 1, 1, 1, 7, 7, 7, 7, 9, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 9, 8, 8, 8, 8, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 1, 1, 1, 1, 5, 5, 5, 5, 9, 2, 2, 2, 2 };
            fakeReels[2] = new[] { 8, 8, 8, 8, 0, 0, 0, 0, 6, 6, 6, 6, 4, 4, 4, 4, 1, 1, 1, 1, 5, 5, 5, 5, 9, 3, 3, 3, 3, 5, 5, 5, 5, 2, 2, 2, 2, 8, 8, 8, 8, 4, 4, 4, 4, 6, 6, 6, 6, 9, 8, 8, 8, 8, 1, 1, 1, 1, 7, 7, 7, 7, 2, 2, 2, 2, 9, 5, 5, 5, 5, 3, 3, 3, 3, 8, 8, 8, 8, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 8, 8, 8, 8, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 8, 8, 8, 8 };
            fakeReels[3] = new[] { 8, 8, 8, 8, 0, 0, 0, 0, 6, 6, 6, 6, 1, 1, 1, 1, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 8, 8, 8, 8, 9, 2, 2, 2, 2, 8, 8, 8, 8, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 9, 6, 6, 6, 6, 3, 3, 3, 3, 8, 8, 8, 8, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 2, 2, 2, 2, 8, 8, 8, 8, 6, 6, 6, 6, 4, 4, 4, 4, 3, 3, 3, 3, 5, 5, 5, 5, 1, 1, 1, 1, 7, 7, 7, 7, 9, 8, 8, 8, 8, 7, 7, 7, 7 };
            fakeReels[4] = new[] { 7, 7, 7, 7, 9, 8, 8, 8, 8, 1, 1, 1, 1, 6, 6, 6, 6, 9, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 3, 3, 3, 3, 2, 2, 2, 2, 7, 7, 7, 7, 9, 6, 6, 6, 6, 8, 8, 8, 8, 5, 5, 5, 5, 7, 7, 7, 7, 1, 1, 1, 1, 8, 8, 8, 8, 4, 4, 4, 4, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 8, 8, 8, 8, 9, 6, 6, 6, 6, 3, 3, 3, 3, 8, 8, 8, 8, 5, 5, 5, 5 };

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
                return WinForWildsLilaWild;
            }
            if (id == 9)
            {
                return WinForScatterLilaWild;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesLilaWild[id, i];
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
