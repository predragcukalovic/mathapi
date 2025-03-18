using MathBaseProject.StructuresV3;

namespace GameWild27
{
    public class MatrixWild27
    {
        #region Private fields

        protected int[,] Matrix;

        #endregion

        #region Public fields

        public static readonly int[] WinForWild27 = { 60, 40, 20, 16, 16, 2, 2, 2, 2 };
        public static int[] PlayLines = { 1 };

        #endregion

        #region Constructor

        public MatrixWild27()
        {
            Matrix = new int[3, 5];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = matrix[i, j];
                }
            }
        }

        public byte[,] GetMatrix()
        {
            var mat = new byte[3, 5];
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = (byte)Matrix[i, j];
                }
            }
            return mat;
        }

        public bool DoubleWin()
        {
            var elem = Matrix[0, 1];
            if (elem < 5)
            {
                return false;
            }
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (elem != Matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Daje dobitni element za liniju
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int GetWinningElementForLine(int line)
        {
            return Matrix[0, (line - 1) / 9 + 1];
        }

        public int GetLineWin(int line)
        {
            var lineNumber = line - 1;
            var r1 = lineNumber / 9 + 1;
            var r2 = (lineNumber / 3) % 3 + 1;
            var r3 = lineNumber % 3 + 1;
            if (Matrix[0, r1] == Matrix[1, r2] && Matrix[1, r2] == Matrix[2, r3])
            {
                return WinForWild27[Matrix[0, r1]];
            }
            return 0;
        }

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[3][];
            fakeReels[0] = new[] { 0, 8, 8, 8, 3, 6, 6, 6, 2, 5, 5, 4, 1, 7, 7, 7, 8, 8, 3, 5, 5, 5, 2, 4, 4, 6, 6, 1, 7, 7, 3 };
            fakeReels[1] = new[] { 0, 8, 8, 8, 3, 6, 6, 6, 2, 5, 5, 4, 1, 7, 7, 7, 8, 8, 3, 5, 5, 5, 2, 4, 4, 6, 6, 1, 7, 7, 3 };
            fakeReels[2] = new[] { 0, 8, 8, 8, 3, 6, 6, 6, 2, 5, 5, 4, 1, 7, 7, 7, 8, 8, 3, 5, 5, 5, 2, 4, 4, 6, 6, 1, 7, 7, 3 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            return new[] { 0, 0, WinForWild27[id] };
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.24,
                symbols = GetHelpSymbolConfigV3(),
                lines = GetHelpLineConfigV3()
            };

            return helpV3;
        }

        private static HelpSymbolConfigV3<object>[] GetHelpSymbolConfigV3()
        {
            var symbols = new HelpSymbolConfigV3<object>[9];
            for (var i = 0; i < 9; i++)
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
            var lines = new HelpLineConfigV3[27];
            for (var i = 0; i < 27; i++)
            {
                lines[i] = new HelpLineConfigV3 { id = i, positions = new int[] { i / 9, (i / 3) % 3, i % 3 } };
            }

            return lines;
        }

        #endregion
    }
}
