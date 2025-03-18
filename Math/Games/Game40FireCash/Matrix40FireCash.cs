using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameTurboHot40;

namespace Game40FireCash
{
    public class Matrix40FireCash : MatrixTurboHot40
    {
        public static readonly int[,] WinForLines40FireCash =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 20, 80, 400},
            {0, 0, 20, 40, 200},
            {0, 0, 20, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWilds40FireCash = { 0, 0, 40, 400, 1000 };
        public static readonly int[] WinForScatter40FireCash = { 0, 0, 5, 20, 500 };
        public static int[] PlayLines = { 40 };
        public static int[] PlayLinesFrenzy = { 4 };

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, GlobalData.GameLineTurbo).CalculateLineWin(WinForLines40FireCash, WinForWilds40FireCash, 1, 1);
        }

        public int GetWinningElementForLine(int lineNumber, int lineWin)
        {
            var elem = GetLine(lineNumber, GlobalData.GameLineTurbo).GetWinningElement(1, lineWin, WinForWilds40FireCash);
            return elem == 0 ? 1 : elem;
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 1, 1, 1, 1, 6, 6, 6, 6, 2, 2, 2, 2, 0, 5, 5, 5, 5, 2, 2, 2, 2, 7, 7, 7, 7, 4, 4, 4, 4, 3, 3, 3, 3, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 0, 3, 3, 3, 3, 6, 6, 6, 6, 2, 2, 2, 2 };
            fakeReels[1] = new[] { 6, 6, 6, 6, 5, 5, 5, 5, 2, 2, 2, 2, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 1, 1, 1, 1, 5, 5, 5, 5, 3, 3, 3, 3, 0, 5, 5, 5, 5, 6, 6, 6, 6, 3, 3, 3, 3, 1, 1, 1, 1, 6, 6, 6, 6, 5, 5, 5, 5, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 0, 7, 7, 7, 7, 2, 2, 2, 2, 5, 5, 5, 5, 0, 4, 4, 4, 4 };
            fakeReels[2] = new[] { 4, 4, 4, 4, 7, 7, 7, 7, 0, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 0, 4, 4, 4, 4, 1, 1, 1, 1, 3, 3, 3, 3, 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 7, 7, 7, 7, 1, 1, 1, 1, 6, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 5, 0, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 6, 7, 7, 7, 7, 5, 5, 5, 5, 2, 2, 2, 2 };
            fakeReels[3] = new[] { 7, 7, 7, 7, 5, 5, 5, 5, 4, 4, 4, 4, 1, 1, 1, 1, 7, 7, 7, 7, 2, 2, 2, 2, 4, 4, 4, 4, 5, 5, 5, 5, 0, 6, 6, 6, 6, 7, 7, 7, 7, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 1, 1, 1, 1, 6, 6, 6, 6, 3, 3, 3, 3, 7, 7, 7, 7, 2, 2, 2, 2, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 5, 6, 6, 6, 6, 0 };
            fakeReels[4] = new[] { 3, 3, 3, 3, 0, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 1, 1, 1, 1, 4, 4, 4, 4, 5, 5, 5, 5, 7, 7, 7, 7, 4, 4, 4, 4, 6, 6, 6, 6, 5, 5, 5, 5, 0, 4, 4, 4, 4, 6, 6, 6, 6, 1, 1, 1, 1, 5, 5, 5, 5, 3, 3, 3, 3, 0, 6, 6, 6, 6, 2, 2, 2, 2, 5, 5, 5, 5, 1, 1, 1, 1, 4, 4, 4, 4, 7, 7, 7, 7, 3, 3, 3, 3, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            if (id == 1)
            {
                return WinForWilds40FireCash;
            }
            if (id == 0)
            {
                return WinForScatter40FireCash;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLines40FireCash[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.23,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
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
