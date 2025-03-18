using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameTwentyFruits
{
    public class MatrixTwentyFruits : Matrix
    {
        #region Constructor
        public MatrixTwentyFruits()
            : base(6)
        {

        }
        #endregion

        #region Public properties

        public static int[] PlayLines = { 20 };

        public static readonly int[,] WinForLinesTwentyFruits =
        {
            {0, 0, 100, 1000, 5000},
            {0, 0, 50, 200, 500},
            {0, 0, 50, 200, 500},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
            {0, 0, 20, 50, 200},
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
            return GetLine(lineNumber, UnicornGlobalData.GameLineTwenties).CalculateLineWin(WinForLinesTwentyFruits, null, -1, 1);
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 4, 4, 4, 1, 1, 1, 0, 1, 1, 3, 3, 3, 0, 3, 3, 5, 5, 5, 2, 2, 5, 5, 2, 2, 2, 4, 4, 4, 1, 1, 1, 0, 0, 0, 0, 0, 1, 3, 3, 3, 3, 5, 5, 2, 5, 2, 2, 2, 4, 4, 6, 6, 6, 4, 3, 3, 3, 5, 5, 5 };
            fakeReels[1] = new[] { 4, 4, 4, 3, 3, 4, 4, 3, 3, 3, 3, 2, 2, 0, 2, 2, 2, 5, 5, 5, 5, 0, 5, 5, 1, 1, 1, 4, 4, 1, 1, 3, 3, 4, 4, 4, 6, 6, 6, 4, 4, 4, 4, 5, 5, 1, 1, 5, 5, 1, 1, 6, 6, 6, 6, 2, 0, 0, 0, 0, 2, 2, 2 };
            fakeReels[2] = new[] { 5, 5, 5, 6, 6, 4, 6, 4, 4, 0, 3, 3, 3, 3, 1, 1, 3, 1, 1, 1, 4, 4, 4, 2, 2, 6, 6, 2, 2, 2, 5, 5, 0, 0, 0, 0, 0, 5, 5, 5, 5, 0, 5, 5, 1, 1, 3, 1, 1, 3, 3, 4, 4, 6, 6, 2, 6, 2, 2, 2 };
            fakeReels[3] = new[] { 5, 3, 3, 0, 3, 3, 0, 2, 2, 2, 5, 5, 4, 4, 4, 6, 6, 4, 4, 6, 6, 6, 6, 1, 1, 1, 5, 5, 3, 3, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 5, 5, 5, 5, 1, 1, 1, 5, 5, 2, 2, 2, 5, 5, 5, 4, 4, 4, 4 };
            fakeReels[4] = new[] { 3, 3, 0, 5, 5, 6, 6, 6, 0, 4, 3, 3, 3, 1, 1, 1, 5, 5, 2, 2, 2, 3, 3, 3, 0, 0, 0, 0, 5, 5, 5, 6, 6, 6, 6, 4, 4, 6, 6, 4, 4, 1, 1, 1, 3, 3, 3, 5, 5, 2, 2, 2, 4, 4, 4 };

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
                coefficients[i] = WinForLinesTwentyFruits[id, i];
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

        private static HelpLineConfigV3[] GetHelpLineConfigV3()
        {
            var lines = new HelpLineConfigV3[20];
            for (var i = 0; i < 20; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = UnicornGlobalData.GameLineTwenties[i, j] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Twenty Fruits.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayTwentyFruits(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

    }
}
