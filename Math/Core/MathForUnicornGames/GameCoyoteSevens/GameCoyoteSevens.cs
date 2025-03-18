using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameCoyoteSevens
{
    public class MatrixCoyoteSevens : Matrix
    {
        #region Constructor

        public MatrixCoyoteSevens() : base(5)
        {
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 5 };

        public static readonly int[,] WinForLinesCoyoteSevens =
        {
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 500, 2500},
            {0, 0, 40, 200, 1000},
            {0, 0, 40, 200, 1000},
            {0, 0, 20, 100, 200},
            {0, 0, 20, 100, 200},
            {0, 5, 20, 50, 200},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesCoyoteSevens, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Coyote Sevens.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayCoyoteSevens(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 6, 6, 6, 1, 5, 5, 0, 1, 4, 4, 6, 6, 3, 3, 6, 0, 5, 5, 1, 4, 4, 5, 5, 1, 3, 3, 3, 5, 5, 5, 1, 2, 2, 5, 5, 2, 2, 5, 5, 2, 0, 5, 5, 5 };
            fakeReels[1] = new[] { 6, 6, 0, 5, 5, 6, 6, 4, 4, 6, 6, 3, 3, 6, 0, 5, 5, 1, 3, 3, 5, 5, 5, 3, 0, 5, 5, 5, 2, 1, 5, 5, 3, 3, 3, 5, 5, 2, 3, 5, 5, 2, 2, 1, 5, 4, 4, 4 };
            fakeReels[2] = new[] { 6, 6, 6, 4, 4, 0, 6, 6, 3, 3, 6, 6, 6, 0, 4, 6, 6, 6, 1, 0, 6, 6, 4, 4, 6, 6, 0, 1, 6, 6, 5, 4, 6, 1, 2, 6, 4, 4, 5, 5, 5, 3, 3 };
            fakeReels[3] = new[] { 0, 6, 6, 5, 5, 6, 6, 6, 2, 2, 4, 4, 1, 6, 6, 0, 4, 6, 6, 4, 4, 6, 6, 1, 2, 6, 3, 3, 0, 5, 5, 5, 4, 4, 4, 2, 5, 5, 3, 3, 5, 5, 5, 4, 4 };
            fakeReels[4] = new[] { 6, 6, 6, 1, 5, 5, 0, 6, 6, 1, 4, 4, 6, 6, 2, 2, 3, 0, 6, 6, 1, 4, 4, 6, 1, 0, 6, 1, 5, 5, 5, 0, 1, 5, 5, 2, 2, 4, 4, 5, 5, 3, 3, 1, 5, 0, 4, 4, 4, 4, 4 };

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
                coefficients[i] = WinForLinesCoyoteSevens[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[7];

            for (var i = 0; i < 7; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i)
                };
            }
            return symbols;
        }

        public static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = UnicornGlobalData.GameLineShifted[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
