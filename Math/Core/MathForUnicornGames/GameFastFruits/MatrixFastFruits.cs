using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameFastFruits
{
    public class MatrixFastFruits : Matrix
    {
        #region Constructor

        public MatrixFastFruits() : base(5)
        {
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] WinForLinesFastFruits =
        {
            {0, 0, 100, 500, 10000},
            {0, 0, 20, 80, 200},
            {0, 0, 20, 80, 200},
            {0, 0, 10, 40, 80},
            {0, 0, 10, 40, 80},
            {0, 0, 10, 20, 40},
            {0, 0, 10, 20, 40},
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
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesFastFruits, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Island Respins.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayFastFruits(int[,] matrix)
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
            fakeReels[0] = new[] { 6, 6, 6, 1, 1, 1, 5, 5, 5, 0, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 2, 2, 2, 5, 5, 5, 0, 6, 6, 6, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3 };
            fakeReels[1] = new[] { 6, 6, 6, 1, 1, 1, 5, 5, 5, 0, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 2, 2, 2, 5, 5, 5, 0, 6, 6, 6, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3 };
            fakeReels[2] = new[] { 6, 6, 6, 1, 1, 1, 5, 5, 5, 0, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 2, 2, 2, 5, 5, 5, 0, 6, 6, 6, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3 };
            fakeReels[3] = new[] { 6, 6, 6, 1, 1, 1, 5, 5, 5, 0, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 2, 2, 2, 5, 5, 5, 0, 6, 6, 6, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3 };
            fakeReels[4] = new[] { 6, 6, 6, 1, 1, 1, 5, 5, 5, 0, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 2, 2, 2, 5, 5, 5, 0, 6, 6, 6, 4, 4, 4, 3, 3, 3, 6, 6, 6, 5, 5, 5, 4, 4, 4, 3, 3, 3 };

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
                coefficients[i] = WinForLinesFastFruits[id, i];
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
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 10; i++)
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
