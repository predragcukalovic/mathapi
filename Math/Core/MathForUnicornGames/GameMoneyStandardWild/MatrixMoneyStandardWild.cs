using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameMoneyStandardWild
{
    public class MatrixMoneyStandardWild : Matrix
    {
        #region Constructor

        public MatrixMoneyStandardWild()
            : base(6)
        {
        }

        #endregion

        #region Public properties

        public static int[] PlayLines = { 20 };

        public static readonly int[,] WinForLinesMoneyStandardWild =
        {
            {0, 0, 0, 0, 0},
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
            {0, 0, 0, 0, 0}
        };
        public static readonly int[] WinForWildMoneyStandardWild = { 0, 0, 100, 1000, 5000 };

        #endregion

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineTwenties).CalculateLineWin(WinForLinesMoneyStandardWild, WinForWildMoneyStandardWild, 0, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Twenty Fruits.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayMoneyStandardWild(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        #region V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 5, 3, 3, 3, 1, 3, 3, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 1, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 2, 2, 6, 6, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, 7, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 3, 4, 4, 4, 4, 0, 0, 0, 0, 6, 6, 6, 6, 2, 6, 2, 2, 2 };
            fakeReels[1] = new[] { 5, 5, 5, 3, 3, 3, 1, 3, 3, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 1, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 2, 2, 6, 6, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, 7, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 3, 4, 4, 4, 4, 0, 0, 0, 0, 6, 6, 6, 6, 2, 6, 2, 2, 2 };
            fakeReels[2] = new[] { 5, 5, 5, 3, 3, 3, 1, 3, 3, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 1, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 2, 2, 6, 6, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, 7, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 3, 4, 4, 4, 4, 0, 0, 0, 0, 6, 6, 6, 6, 2, 6, 2, 2, 2 };
            fakeReels[3] = new[] { 5, 5, 5, 3, 3, 3, 1, 3, 3, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 1, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 2, 2, 6, 6, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, 7, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 3, 4, 4, 4, 4, 0, 0, 0, 0, 6, 6, 6, 6, 2, 6, 2, 2, 2 };
            fakeReels[4] = new[] { 5, 5, 5, 3, 3, 3, 1, 3, 3, 7, 7, 7, 7, 0, 0, 0, 0, 4, 4, 4, 1, 4, 4, 6, 6, 6, 6, 6, 0, 0, 0, 0, 2, 2, 6, 6, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, 7, 0, 0, 0, 3, 3, 3, 1, 1, 1, 1, 3, 4, 4, 4, 4, 0, 0, 0, 0, 6, 6, 6, 6, 2, 6, 2, 2, 2 };

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
                return WinForWildMoneyStandardWild;
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesMoneyStandardWild[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[8];
            for (var i = 0; i < 8; i++)
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

        #endregion
    }
}
