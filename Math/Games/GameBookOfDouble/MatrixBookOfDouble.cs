using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameMagicOfTheRing;

namespace GameBookOfDouble
{
    public class MatrixBookOfDouble : MatrixMagicOfTheRing
    {
        #region Public properties

        public static int[] PlayLines = { 10 };
        public new const int GRATIS_GAMES = 10;
        public static readonly int[,] WinForLinesBookOfDouble =
        {
            {0, 0, 0, 0, 0},
            {0, 10, 100, 1000, 5000},
            {0, 5, 40, 400, 2000},
            {0, 5, 30, 100, 750},
            {0, 5, 30, 100, 750},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 40, 150},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 5, 25, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForGratisBookOfDouble = { 0, 0, 2, 25, 250 };
        public static readonly int[] WinForWildsBookOfDouble = { 0, 10, 100, 1000, 5000 };

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin(WinForLinesBookOfDouble, WinForWildsBookOfDouble, 0, 1);
        }

        /// <summary>
        /// Računa dobitak linije sa bonus simbolom.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="gratisElement1"></param>
        /// <param name="gratisElement2"></param>
        /// <returns></returns>
        public int CalculateWinLine(int lineNumber, int gratisElement1, int gratisElement2)
        {
            if (gratisElement1 == 0 && gratisElement2 == 0)
            {
                return CalculateWinLine(lineNumber);
            }
            var line = GetLine(lineNumber);
            for (var i = 0; i < 5; i++)
            {
                var elem = line.GetElement(i);
                if (elem == gratisElement1 || elem == gratisElement2)
                {
                    line.SetElement(i, 15);
                }
            }
            return line.CalculateLineWin(WinForLinesBookOfDouble, WinForWildsBookOfDouble, 0, 1);
        }

        /// <summary>
        /// Klon matrice.
        /// </summary>
        /// <returns></returns>
        public MatrixBookOfDouble Clone()
        {
            var mat = new MatrixBookOfDouble();
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    mat.SetElement(i, j, GetElement(i, j));
                }
            }
            return mat;
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
            fakeReels[0] = new[] { 9, 8, 7, 2, 9, 7, 8, 0, 6, 4, 9, 8, 6, 2, 8, 7, 6, 5, 7, 8, 3, 9, 5, 7, 6, 8, 5, 7, 9, 0, 5, 8, 9, 7, 1 };
            fakeReels[1] = new[] { 9, 8, 7, 0, 5, 8, 7, 2, 6, 5, 8, 6, 9, 8, 4, 7, 5, 6, 9, 2, 8, 6, 5, 7, 4, 9, 5, 1, 8, 9, 5, 7, 3, 0, 4 };
            fakeReels[2] = new[] { 9, 8, 5, 0, 9, 6, 2, 5, 7, 4, 8, 9, 4, 6, 5, 4, 7, 5, 1, 6, 9, 2, 7, 6, 8, 3, 7, 2, 6, 8, 3, 5, 9, 2, 0 };
            fakeReels[3] = new[] { 9, 8, 4, 6, 9, 1, 0, 7, 5, 2, 8, 7, 3, 8, 6, 4, 5, 6, 1, 7, 3, 6, 2, 9, 7, 3, 9, 6, 8, 2, 5, 9, 4, 0, 1 };
            fakeReels[4] = new[] { 9, 8, 4, 5, 6, 4, 9, 5, 8, 1, 5, 8, 3, 7, 5, 4, 9, 8, 0, 6, 8, 7, 5, 4, 7, 9, 2, 5, 3, 7, 6, 1, 9, 0, 2 };

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
                return WinForGratisBookOfDouble;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesBookOfDouble[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.2,
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
