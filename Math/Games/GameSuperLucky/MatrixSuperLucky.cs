using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameCloverCash;

namespace GameSuperLucky
{
    public class MatrixSuperLucky : MatrixCloverCash
    {
        #region Public properties

        public static readonly int[,] WinForLinesSuperLucky =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 20, 100, 1250},
            {0, 0, 10, 50, 250},
            {0, 0, 10, 50, 250},
            {0, 0, 10, 50, 200},
            {0, 0, 10, 50, 200},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 20, 100},
            {0, 0, 5, 10, 50},
            {0, 0, 5, 10, 50},
            {0, 0, 5, 10, 50},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildsSuperLucky = { 0, 0, 20, 100, 1250 };

        public static new int[] PlayLines = { 5 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesSuperLucky, WinForWildsSuperLucky, 0, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static new int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 10, 10, 7, 7, 3, 3, 8, 10, 10, 10, 5, 6, 6, 5, 7, 10, 11, 11, 11, 4, 9, 2, 2, 2, 9, 9, 9, 1, 9, 9, 4, 8, 8, 8, 7, 10, 10, 11, 11, 11, 7, 7, 0, 0, 0, 0, 9, 9, 9, 4, 7, 10, 1, 1, 1, 10 };
            fakeReels[1] = new[] { 9, 9, 9, 2, 5, 2, 5, 5, 11, 11, 11, 4, 4, 6, 8, 6, 8, 6, 6, 10, 10, 8, 8, 8, 4, 8, 10, 10, 10, 3, 3, 11, 11, 11, 8, 8, 7, 7, 7, 4, 7, 1, 1, 1, 9, 9, 9, 2, 2, 5, 2, 5, 5, 0, 0, 0, 0 };
            fakeReels[2] = new[] { 4, 7, 4, 4, 3, 2, 2, 9, 9, 9, 11, 11, 11, 8, 5, 5, 5, 10, 5, 10, 10, 10, 3, 0, 0, 0, 11, 11, 11, 3, 1, 1, 1, 9, 2, 9, 9, 9, 8, 6, 8, 8, 8, 6, 6, 7, 7, 7, 4, 7, 4, 1, 9, 0, 0, 0, 5, 5 };
            fakeReels[3] = new[] { 5, 5, 7, 5, 7, 7, 7, 4, 4, 5, 7, 11, 11, 11, 3, 2, 8, 8, 3, 8, 8, 8, 2, 9, 2, 9, 9, 9, 1, 9, 9, 0, 0, 0, 0, 10, 1, 1, 1, 4, 10, 10, 10, 6, 4, 6, 6, 6, 6, 11, 11, 11, 5, 5, 7, 5, 7, 7, 7, 5, 7 };
            fakeReels[4] = new[] { 6, 6, 6, 4, 4, 6, 4, 9, 9, 9, 0, 11, 11, 11, 8, 8, 2, 8, 2, 8, 5, 1, 1, 1, 2, 7, 7, 7, 5, 5, 5, 10, 5, 10, 10, 10, 3, 10, 3, 3, 7, 0, 0, 0, 0, 9, 6, 6, 6, 6, 4, 4, 6, 11, 11, 11, 8, 8 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static new int[][] GetGratisFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 11 };
            fakeReels[1] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 11 };
            fakeReels[2] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12 };
            fakeReels[3] = new[] { 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12 };
            fakeReels[4] = new[] { 12, 12, 12, 12, 12, 12, 12, 12, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11 };

            return fakeReels;
        }

        public static new HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.0,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
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
                return WinForWildsSuperLucky;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesSuperLucky[id, i];
            }
            return coefficients;
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
                    features = i == 11 ? new[] { HelpSymbolFeatureV3.Bonus } : new[] { HelpSymbolFeatureV3.Regular }
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
    }
}
