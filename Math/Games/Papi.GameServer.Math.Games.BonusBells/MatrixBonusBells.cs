using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace Papi.GameServer.Math.Games.BonusBells
{
    public class MatrixBonusBells : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesBonusBells =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 100, 500, 4000},
            {0, 0, 40, 200, 800},
            {0, 0, 30, 100, 500},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 8, 20, 80},
            {0, 0, 8, 20, 80},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildBonusBells = { 0, 0, 50, 250, 6000 };
        public static readonly int[] WinForGratisBonusBells = { 0, 0, 1, 10, 500 };
        public const int GRATIS_GAMES = 6;
        public static int[] PlayLines = { 10 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesBonusBells, WinForWildBonusBells, 0, 1);
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
            fakeReels[0] = new[] { 0, 0, 0, 0, 0 };
            fakeReels[1] = new[] { 8, 8, 5, 5, 5, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[2] = new[] { 8, 8, 5, 5, 5, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 6, 5, 5, 0, 7, 7, 4, 4, 4, 2, 2, 2, 0, 1, 1, 1, 8, 8, 8, 4, 4, 4, 6, 6, 6, 2, 2, 3, 3, 3, 0, 4, 4 };
            fakeReels[3] = new[] { 1, 1, 1, 3, 3, 3, 0, 5, 5, 6, 6, 6, 0, 4, 4, 4, 2, 2, 3, 3, 5, 5, 5, 7, 7, 0, 1, 1, 1, 8, 8, 8, 7, 7, 7, 2, 2, 2, 2, 4, 4, 6, 6, 6, 8, 8 };
            fakeReels[4] = new[] { 0, 0, 0, 0, 0 };
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
                return new[] {0, 0, 0, 0, WinForWildBonusBells[4]};
            }
            if (id == 9)
            {
                return WinForGratisBonusBells;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBonusBells[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?) 96.25,
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
