using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace GameElGrandeToro
{
    public class MatrixElGrandeToro : MathBaseProject.BaseMathData.Matrix
    {
        #region Public properties

        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesElGrandeToro =
        {
            {0, 0, 0, 0, 0},
            {0, 2, 20, 100, 500},
            {0, 0, 15, 50, 250},
            {0, 0, 15, 50, 250},
            {0, 0, 15, 50, 250},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildElGrandeToro = { 0, 10, 50, 200, 1000 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesElGrandeToro, WinForWildElGrandeToro, 0, 1);
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
            fakeReels[0] = new[] { 9, 8, 7, 10, 2, 9, 7, 8, 0, 6, 4, 9, 8, 10, 6, 2, 8, 7, 6, 5, 7, 8, 3, 9, 5, 7, 6, 8, 5, 7, 9, 0, 5, 8, 9, 7, 1, 1, 1, 1 };
            fakeReels[1] = new[] { 9, 8, 7, 0, 5, 8, 7, 2, 6, 5, 8, 6, 9, 8, 4, 7, 5, 6, 9, 2, 8, 6, 5, 7, 4, 9, 5, 1, 1, 1, 1, 8, 9, 5, 7, 3, 0, 4 };
            fakeReels[2] = new[] { 9, 8, 5, 0, 9, 6, 2, 5, 7, 4, 8, 9, 4, 6, 5, 4, 7, 5, 1, 1, 1, 1, 6, 9, 2, 10, 7, 6, 8, 3, 7, 2, 6, 8, 3, 5, 9, 2, 0 };
            fakeReels[3] = new[] { 9, 8, 4, 6, 9, 1, 1, 1, 1, 0, 7, 5, 2, 8, 7, 3, 8, 6, 4, 5, 6, 1, 1, 1, 1, 7, 3, 6, 2, 9, 7, 3, 9, 6, 8, 2, 5, 9, 4, 0 };
            fakeReels[4] = new[] { 9, 8, 4, 10, 5, 6, 4, 9, 5, 8, 1, 1, 1, 1, 5, 8, 3, 7, 10, 5, 4, 9, 8, 0, 6, 8, 7, 5, 4, 7, 9, 2, 5, 3, 7, 6, 1, 1, 1, 1, 9, 0, 2 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja u bonusu
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsGratis()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 9, 8, 7, 2, 9, 7, 8, 0, 6, 4, 9, 8, 6, 2, 8, 7, 0, 6, 5, 7, 8, 3, 9, 5, 7, 6, 8, 5, 7, 9, 0, 5, 8, 9, 7, 1, 1, 1, 1 };
            fakeReels[1] = new[] { 9, 8, 7, 0, 5, 8, 7, 2, 6, 5, 8, 6, 9, 0, 8, 4, 7, 5, 6, 9, 2, 8, 6, 5, 0, 7, 4, 9, 5, 1, 1, 1, 1, 8, 9, 5, 7, 3, 0, 4 };
            fakeReels[2] = new[] { 9, 8, 5, 0, 9, 6, 2, 5, 7, 4, 8, 9, 4, 6, 5, 0, 4, 7, 5, 1, 1, 1, 1, 6, 9, 2, 7, 6, 0, 8, 3, 7, 2, 6, 8, 3, 5, 9, 2, 0 };
            fakeReels[3] = new[] { 9, 8, 0, 4, 6, 9, 1, 1, 1, 1, 0, 7, 5, 2, 8, 7, 3, 8, 6, 0, 4, 5, 6, 1, 1, 1, 1, 7, 3, 6, 2, 9, 7, 3, 0, 9, 6, 8, 2, 5, 9, 4, 0 };
            fakeReels[4] = new[] { 9, 8, 4, 5, 0, 6, 4, 9, 5, 8, 1, 1, 1, 1, 5, 8, 3, 7, 0, 5, 4, 9, 8, 0, 6, 8, 7, 5, 4, 7, 0, 9, 2, 5, 3, 7, 6, 1, 1, 1, 1, 9, 0, 2 };

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
                return WinForWildElGrandeToro;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesElGrandeToro[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[11];
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
            symbols[10] = new HelpSymbolConfigV3<object>
            {
                id = 10,
                extra = new HelpSymbolExtraV3(),
                coefficients = GetSymbolCoefficients(10),
                features = new[] { HelpSymbolFeatureV3.FreeSpin }
            };

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
