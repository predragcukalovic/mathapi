using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;

namespace GameMegaHot
{
    public class MatrixMegaHot10 : Matrix
    {
        #region Public properties

        public static readonly int[,] WinForLinesMegaHot =
        {
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 300, 1000},
            {0, 0, 30, 80, 300},
            {0, 0, 20, 50, 200},
            {0, 0, 10, 40, 120},
            {0, 0, 10, 30, 100},
            {0, 0, 10, 30, 100}
        };
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
            return GetLine(lineNumber, GlobalData.GameLineExtra).CalculateLineWin(WinForLinesMegaHot, null, -1, 1);
        }

        /// <summary>
        /// Daje sa koliko se množi dobitak (1, 3, 4 ili 5).
        /// </summary>
        /// <returns></returns>
        public int GetMultiplicator()
        {
            var firstElement = GetElement(0, 0);
            if (firstElement >= 2)
            {
                var next = 0;
                while (next < 15 && GetElement(next / 3, next % 3) == firstElement)
                {
                    next++;
                }
                if (next >= 9)
                {
                    return next / 3;
                }
            }
            return 1;
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
            fakeReels[0] = new[] { 4, 4, 4, 4, 4, 0, 5, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 3, 3, 3, 3, 3, 0, 1, 6, 6, 6, 6, 6, 2, 2, 2, 2, 2, 1, 3, 3, 3, 3, 6, 6, 6, 6, 6, 0, 5, 5, 5, 5, 5, 0 };
            fakeReels[1] = new[] { 0, 3, 3, 3, 3, 5, 5, 5, 5, 1, 1, 4, 4, 4, 4, 6, 6, 6, 6, 2, 2, 2, 2, 0, 3, 3, 3, 3, 3, 0, 1, 2, 2, 2, 2, 1, 1, 4, 4, 4, 4, 5, 5, 5, 5, 5, 0, 6, 6, 6, 6, 6, 6, 2, 2, 2, 2, 2, 2 };
            fakeReels[2] = new[] { 6, 6, 6, 6, 6, 1, 1, 2, 2, 2, 2, 0, 3, 3, 3, 3, 3, 4, 4, 4, 4, 0, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 1, 1, 0, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 0 };
            fakeReels[3] = new[] { 2, 2, 2, 2, 2, 0, 3, 3, 3, 3, 3, 1, 1, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 0, 2, 2, 2, 2, 2, 1, 0, 4, 4, 4, 4, 4, 1, 6, 6, 6, 6, 6, 0, 4, 4, 4, 4, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 2, 2, 0, 3, 3, 3, 3, 3 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 3, 0, 5, 5, 5, 5, 5, 5, 1, 1, 0, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 6, 6, 6, 6, 0, 2, 2, 2, 2, 2, 1, 1, 0, 4, 4, 4, 4, 4, 0, 2, 2, 2, 2, 2, 2 };
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
                coefficients[i] = WinForLinesMegaHot[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.48,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[7];
            for (var i = 0; i < 7; i++)
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

