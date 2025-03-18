using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace GameKingOfThunder
{
    public class MatrixKingOfThunder : Matrix
    {
        #region Public properties

        public const int GRATIS_GAMES = 6;
        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesKingOfThunder =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 150, 300, 1000},
            {0, 0, 0, 0, 0},
            {0, 0, 60, 150, 600},
            {0, 0, 30, 60, 250},
            {0, 0, 30, 60, 250},
            {0, 0, 30, 60, 250},
            {0, 0, 15, 30, 100},
            {0, 0, 15, 30, 100},
            {0, 0, 10, 20, 75},
            {0, 0, 10, 20, 75},
            {0, 0, 10, 20, 75},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildKingOfThunder = { 0, 0, 150, 300, 3000 };
        public static readonly int[] WinForGratisKingOfThunder = { 0, 0, 2, 10, 1000 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber">Broj linije.</param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesKingOfThunder, WinForWildKingOfThunder, 0, 1);
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
            fakeReels[0] = new[] { 1, 4, 9, 2, 3, 11, 8, 7, 1, 10, 3, 8, 7, 11, 3, 0, 9, 6, 10, 3, 5, 2, 8, 7, 9, 5, 0, 7, 3, 6, 11, 4, 8, 1, 10, 3, 11, 4, 1, 8, 11, 5, 7, 9, 4, 10, 5, 7, 6, 10, 0, 4, 8, 6, 11, 9, 1, 7 };
            fakeReels[1] = new[] { 10, 8, 3, 2, 6, 9, 11, 1, 5, 10, 11, 4, 0, 6, 10, 8, 11, 7, 1, 9, 6, 11, 4, 7, 5, 3, 9, 8, 5, 0, 3, 8, 11, 9, 6, 11, 1, 8, 5, 11, 10, 5, 6, 7, 0, 4, 9, 10, 3, 5, 10, 9, 7, 4, 2, 10, 3 };
            fakeReels[2] = new[] { 1, 8, 7, 3, 5, 1, 9, 7, 5, 11, 3, 8, 1, 10, 4, 2, 9, 1, 5, 7, 11, 4, 6, 9, 10, 4, 3, 0, 6, 8, 5, 9, 10, 4, 6, 2, 8, 9, 5, 8, 6, 11, 7, 10, 9, 8, 5, 4, 11, 6, 7, 0, 10, 8, 11, 9 };
            fakeReels[3] = new[] { 4, 11, 7, 0, 9, 8, 1, 7, 11, 4, 8, 9, 6, 5, 10, 8, 4, 2, 5, 10, 7, 1, 9, 6, 3, 10, 8, 4, 11, 6, 3, 9, 5, 8, 3, 0, 5, 9, 10, 4, 11, 7, 3, 2, 9, 6, 4, 0, 11, 10, 3, 11, 9, 10 };
            fakeReels[4] = new[] { 3, 11, 7, 4, 10, 8, 0, 4, 6, 9, 3, 7, 2, 5, 9, 1, 10, 11, 4, 9, 5, 4, 0, 3, 5, 10, 6, 1, 10, 7, 3, 11, 8, 6, 0, 7, 11, 5, 1, 9, 8, 1, 5, 8, 6, 11, 4, 7, 8, 10, 6, 9 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusu
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 6, 9, 7, 10, 0, 8, 9, 4, 8, 10, 3, 5, 7, 1, 3, 0, 6, 11, 7, 10, 8, 5, 6, 9, 7, 11, 3, 10, 7, 4, 11, 0, 5, 10, 6, 9, 1, 8, 10, 9, 6, 4, 11, 9, 5, 8, 1, 6, 3, 7, 11, 8 };
            fakeReels[1] = new[] { 11, 8, 4, 0, 11, 10, 6, 11, 8, 1, 9, 5, 11, 10, 7, 0, 8, 9, 11, 3, 7, 8, 10, 6, 9, 5, 11, 4, 6, 10, 9, 8, 3, 10, 1, 7, 6, 1, 3, 10, 11, 4, 7, 1, 3, 8, 10, 4, 6, 9, 0, 5, 3 };
            fakeReels[2] = new[] { 10, 3, 11, 6, 7, 11, 0, 4, 10, 6, 9, 11, 7, 10, 8, 4, 11, 9, 5, 10, 3, 1, 8, 6, 10, 5, 9, 10, 4, 8, 9, 3, 6, 0, 4, 8, 10, 7, 11, 3, 7, 11, 8, 1, 5, 0, 3, 9, 6, 11, 8, 5, 4 };
            fakeReels[3] = new[] { 8, 3, 9, 6, 0, 4, 11, 5, 10, 8, 9, 3, 7, 11, 6, 4, 10, 3, 8, 9, 7, 10, 3, 0, 6, 9, 5, 11, 3, 1, 8, 7, 4, 10, 0, 1, 8, 9, 11, 10, 7, 6, 10, 5, 1, 7, 9, 11, 4, 10 };
            fakeReels[4] = new[] { 6, 11, 9, 7, 0, 8, 10, 3, 11, 7, 9, 4, 1, 8, 10, 6, 9, 11, 8, 10, 4, 6, 10, 0, 8, 5, 11, 6, 9, 4, 1, 3, 10, 5, 6, 0, 3, 8, 9, 4, 11, 7, 9, 3, 10, 7, 9, 4, 6, 7, 11, 5 };

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
                return WinForWildKingOfThunder;
            }
            if (id == 2)
            {
                return WinForGratisKingOfThunder;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesKingOfThunder[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.21,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[12];
            for (var i = 0; i < 12; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i),
                    features = new[] { i == 2 ? HelpSymbolFeatureV3.FreeSpin : HelpSymbolFeatureV3.Regular }
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
