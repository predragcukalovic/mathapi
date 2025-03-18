using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using MathForUnicornGames.BasicUnicornData;

namespace MathForUnicornGames.GameBigSpinSevens
{
    public class MatrixBigSpinSevens : Matrix
    {
        public static int[] PlayLines = { 5 };
        public static readonly int[,] WinForLinesBigSpinSevens =
        {
            {0, 0, 0, 0, 0 },
            {0, 0, 100, 200, 1000},
            {0, 0, 100, 200, 1000},
            {0, 0, 100, 200, 1000},
            {0, 0, 25, 125, 400 },
            {0, 0, 10, 100, 300 },
            {0, 0, 5, 40, 100}
        };

        public MatrixBigSpinSevens() : base(7)
        {
        }

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public override int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber, UnicornGlobalData.GameLineDoubleShift).CalculateLineWin(WinForLinesBigSpinSevens, null, -1, 1);
        }

        /// <summary>
        /// Konstruiše matricu na osnovu dvodimenzionalnog niza za igru Big Spin Sevens.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArrayBigSpinSevens(int[,] matrix)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }

        public void TransformMatrix()
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 2; j < 5; j++)
                {
                    if (GetElement(i, j) > 9)
                    {
                        SetElement(i, j, GetElement(i, j) / 10);
                    }
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
            fakeReels[0] = new[] { 30, 5, 6, 6, 4, 4, 4, 30, 5, 5, 20, 21, 30, 5, 6, 5, 5, 30, 20, 21, 5, 6, 30, 4, 4, 5, 6, 30, 6, 6, 5, 6, 6, 5, 6, 20, 21, 5, 5, 5, 10, 11, 12, 4, 4, 4, 6, 6, 6, 30, 10, 11, 12, 6, 6, 20, 21, 6, 6 };
            fakeReels[1] = new[] { 30, 5, 6, 6, 4, 4, 4, 30, 5, 5, 20, 21, 30, 5, 6, 5, 5, 30, 20, 21, 5, 6, 30, 4, 4, 5, 6, 30, 6, 6, 5, 6, 6, 5, 6, 20, 21, 5, 5, 10, 11, 12, 4, 4, 6, 6, 30, 10, 11, 12, 6, 6, 20, 21, 6, 6 };
            fakeReels[2] = new[] { 30, 5, 6, 6, 4, 4, 4, 30, 5, 5, 20, 21, 30, 5, 6, 5, 5, 30, 20, 21, 5, 6, 30, 4, 4, 5, 6, 30, 6, 6, 5, 6, 6, 5, 6, 20, 21, 5, 5, 10, 11, 12, 4, 4, 4, 6, 6, 6, 30, 10, 11, 12, 6, 6, 20, 21, 6, 6 };
            fakeReels[3] = new[] { 30, 5, 6, 6, 4, 4, 4, 30, 5, 5, 20, 21, 30, 5, 6, 5, 5, 30, 20, 21, 5, 6, 30, 4, 4, 5, 6, 30, 6, 6, 5, 6, 6, 5, 6, 20, 21, 5, 5, 5, 10, 11, 12, 4, 6, 6, 6, 30, 10, 11, 12, 6, 6, 20, 21, 6, 6 };
            fakeReels[4] = new[] { 30, 5, 6, 6, 4, 4, 4, 30, 5, 5, 20, 21, 30, 5, 6, 5, 5, 30, 20, 21, 5, 6, 30, 4, 4, 5, 6, 30, 6, 6, 5, 6, 6, 6, 5, 6, 20, 21, 5, 5, 5, 10, 11, 12, 4, 4, 4, 6, 6, 6, 30, 10, 11, 12, 6, 6, 20, 21, 6, 6 };

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
                coefficients[i] = WinForLinesBigSpinSevens[id, i];
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
            var symbols = new HelpSymbolConfigV3<object>[6];

            for (var i = 0; i < 6; i++)
            {
                symbols[i] = new HelpSymbolConfigV3<object>
                {
                    id = i + 1,
                    features = new[] { HelpSymbolFeatureV3.Regular },
                    extra = new HelpSymbolExtraV3(),
                    coefficients = GetSymbolCoefficients(i + 1)
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
                    pos[j] = UnicornGlobalData.GameLineDoubleShift[i, j] - 2;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }
        #endregion
    }
}
