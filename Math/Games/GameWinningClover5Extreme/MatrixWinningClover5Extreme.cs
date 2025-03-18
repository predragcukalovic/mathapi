using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameBurstingHot5;

namespace GameWinningClover5Extreme
{
    public class MatrixWinningClover5Extreme : MatrixBurstingHot5
    {
        #region Public properties WinningClover5Extreme

        public static readonly int[,] WinForLinesWinningClover5Extreme =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 100, 500, 5000},
            {0, 0, 50, 250, 500},
            {0, 0, 50, 250, 500},
            {0, 0, 25, 100, 250},
            {0, 0, 10, 50, 150},
            {0, 0, 10, 50, 150},
            {0, 0, 10, 50, 150},
            {0, 0, 10, 50, 150},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildWinningClover5Extreme = { 0, 0, 0, 0, 0 };
        public static readonly int[] WinForScatter1WinningClover5Extreme = { 0, 0, 3, 20, 100 };
        public const int WIN_FOR_SCATTER2_WINNING_CLOVER5_EXTREME = 20;

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesWinningClover5Extreme, WinForWildWinningClover5Extreme, 0, 1);
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static new int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 8, 8, 8, 4, 4, 4, 10, 3, 3, 3, 1, 1, 1, 9, 7, 7, 7, 5, 5, 5, 6, 6, 6, 2, 2, 2, 6, 4, 4, 1, 6, 6, 1, 7, 7, 7, 9, 4, 4, 4, 5, 8, 8, 1, 1, 1, 2, 2, 2, 5, 5, 10, 7, 7, 4, 4, 4, 2, 2, 1, 3, 3, 4, 4, 4, 7, 6, 6, 6, 3, 3, 3, 9 };
            fakeReels[1] = new[] { 1, 1, 1, 9, 8, 8, 5, 5, 0, 6, 6, 6, 3, 3, 4, 4, 5, 5, 5, 8, 8, 7, 7, 1, 1, 1, 2, 2, 2, 0, 3, 3, 3, 9, 7, 7, 8, 8, 8, 8, 1, 1, 1, 3, 3, 2, 2, 2, 8, 7, 7, 6, 6, 6, 5, 5, 7, 4, 4, 4, 7, 7, 4, 4, 0, 8, 8, 8, 6, 6, 9, 1, 1 };
            fakeReels[2] = new[] { 5, 5, 5, 9, 4, 4, 4, 3, 10, 7, 7, 1, 1, 5, 5, 5, 10, 5, 5, 5, 4, 6, 6, 6, 2, 2, 4, 4, 4, 7, 7, 1, 1, 1, 9, 8, 8, 8, 0, 7, 7, 6, 6, 6, 1, 1, 1, 7, 8, 8, 8, 9, 5, 5, 5, 10, 7, 7, 7, 0, 4, 4, 4, 1, 6, 6, 9, 3, 3, 3, 0, 2, 2, 2 };
            fakeReels[3] = new[] { 1, 1, 1, 5, 5, 9, 3, 3, 4, 4, 4, 0, 5, 5, 5, 9, 6, 6, 6, 8, 8, 8, 4, 4, 2, 2, 8, 8, 0, 7, 7, 7, 6, 6, 6, 9, 4, 4, 4, 0, 7, 7, 7, 3, 3, 0, 4, 4, 6, 6, 6, 1, 1, 2, 2, 2, 9, 8, 8, 8, 5, 5, 5, 7, 7, 7, 4, 4, 4, 5, 5, 2, 2, };
            fakeReels[4] = new[] { 2, 2, 4, 4, 7, 7, 5, 5, 5, 9, 8, 8, 7, 7, 7, 10, 5, 5, 5, 9, 2, 2, 2, 8, 8, 8, 10, 5, 1, 1, 1, 9, 6, 4, 4, 4, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 6, 6, 6, 4, 4, 4, 5, 5, 5, 5, 4, 8, 8, 8, 4, 4, 4, 9, 3, 3, 3, 10, 4, 4, 3, 3, 1, 1 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static new int[] GetSymbolCoefficients(int id)
        {
            if (id == 0)
            {
                return WinForWildWinningClover5Extreme;
            }
            if (id == 9)
            {
                return WinForScatter1WinningClover5Extreme;
            }
            if (id == 10)
            {
                return new[] { 0, 0, WIN_FOR_SCATTER2_WINNING_CLOVER5_EXTREME, 0, 0 };
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesWinningClover5Extreme[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.5,
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
