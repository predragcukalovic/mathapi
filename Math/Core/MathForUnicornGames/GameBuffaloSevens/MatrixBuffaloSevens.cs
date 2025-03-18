using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameBuffaloSevens
{
    public class MatrixBuffaloSevens : Matrix
    {
        public MatrixBuffaloSevens() : base(5) { }

        #region Public properties

        public static int[] PlayLines = { 10 };
        public static readonly int[,] WinForLinesBuffaloSevens =
        {
            {0, 10, 100, 250, 500 },
            {0, 0, 200, 500, 2500},
            {0, 0, 40, 100, 400},
            {0, 0, 40, 100, 400},
            {0, 0, 20, 50, 200 },
            {0, 0, 20, 50, 200 },
            {0, 0, 10, 40, 200},
            {0, 0, 10, 40, 200}
        };

        #endregion

        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineShifted).CalculateLineWin(WinForLinesBuffaloSevens, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Buffalo Sevens.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayBuffaloSevens(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public bool FullDeck()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (GetElement(i, j) != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region Struct V3

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 0, 0, 0, 7, 7, 4, 4, 0, 0, 0, 5, 5, 6, 6, 0, 0, 0, 5, 5, 7, 7, 0, 0, 0, 6, 6, 4, 4, 7, 7, 2, 2, 1, 7, 7, 7, 1, 3, 3, 3, 1, 5, 5, 5, 1, 2, 2, 2, 1, 6, 6, 6, 1, 4, 4, 4, 1, 6, 6, 6, 5, 5, 5, 2, 2, 2, 7, 7, 4, 4, 4, 6, 4, 6, 6, 6, 7, 7, 7, 3, 3, 6, 3, 6, 6, 6 };
            fakeReels[1] = new[] { 0, 0, 0, 4, 4, 7, 7, 0, 0, 0, 6, 6, 5, 5, 5, 0, 0, 0, 4, 4, 6, 6, 1, 2, 2, 7, 7, 1, 3, 3, 3, 3, 1, 5, 5, 5, 1, 2, 2, 2, 0, 0, 0, 6, 6, 6, 1, 2, 2, 2, 1, 3, 3, 3, 1, 6, 6, 6, 1, 7, 7, 7, 1, 3, 3, 3, 1, 2, 2, 2, 1, 6, 6, 6, 3, 3, 3, 4, 3, 4, 4 };
            fakeReels[2] = new[] { 0, 0, 0, 7, 7, 4, 4, 5, 5, 6, 6, 0, 0, 0, 5, 5, 7, 7, 0, 0, 0, 6, 6, 4, 4, 1, 7, 7, 2, 2, 1, 7, 7, 7, 1, 3, 3, 3, 1, 5, 5, 5, 1, 2, 2, 2, 1, 6, 6, 6, 0, 0, 0, 4, 4, 4, 1, 6, 6, 6, 5, 5, 5, 2, 2, 2, 7, 7, 4, 4, 4, 0, 0, 0, 6, 6, 6, 7, 7, 7, 3, 3, 6, 3, 6, 6, 6 };
            fakeReels[3] = new[] { 0, 0, 0, 4, 4, 7, 7, 0, 0, 0, 6, 6, 5, 5, 5, 0, 0, 0, 4, 4, 6, 6, 1, 2, 2, 7, 7, 1, 3, 3, 3, 3, 1, 5, 5, 5, 1, 2, 2, 2, 1, 6, 6, 6, 1, 2, 2, 2, 1, 3, 3, 3, 1, 6, 6, 6, 1, 7, 7, 7, 0, 0, 0, 0, 3, 3, 3, 1, 2, 2, 2, 1, 6, 6, 6, 3, 3, 3, 4, 3, 4, 4 };
            fakeReels[4] = new[] { 0, 0, 0, 7, 7, 4, 4, 0, 0, 0, 5, 5, 6, 6, 0, 0, 0, 5, 5, 7, 7, 0, 0, 0, 6, 6, 4, 4, 1, 7, 7, 2, 2, 0, 0, 0, 7, 7, 7, 1, 3, 3, 3, 1, 5, 5, 5, 1, 2, 2, 2, 1, 6, 6, 6, 1, 4, 4, 4, 1, 6, 6, 6, 5, 5, 5, 2, 2, 2, 7, 7, 4, 4, 4, 0, 0, 0, 6, 6, 6, 7, 7, 7, 3, 3, 6, 3, 6, 6, 6 };

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
                coefficients[i] = WinForLinesBuffaloSevens[id, i];
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
