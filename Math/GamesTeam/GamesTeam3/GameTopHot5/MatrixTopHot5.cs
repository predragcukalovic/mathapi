using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;
using MathBaseProject.StructuresV3;
namespace GameTopHot5
{
    public class MatrixTopHot5 : MatrixVegasHot
    {
        #region Public fields

        public static readonly int[] WinForTopHot5 = { 300, 200, 100, 80, 80, 40, 40, 40, 40 };
        public static int[] PlayLines = { 5 };

        #endregion
        public MatrixTopHot5()
        {
            Matrix = new int[3, 5];
        }

        #region Private methods

        private LineTopHot5 GetLine(int numberOfLine)
        {
            var line = new LineTopHot5();
            for (var i = 0; i < 3; i++)
            {
                line.SetElement(i, Matrix[i, GlobalData.GameLineVegasHot[numberOfLine - 1, i] + 1]);
            }
            return line;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Računa dobitak linije.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        public new int CalculateWinOfLine(int numberOfLine)
        {
            var line = GetLine(numberOfLine);
            return line.CalculateLineWin();
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

        /// <summary>
        /// Transformiše dvodimenzionalni niz u matricu.
        /// </summary>
        /// <param name="matrix"></param>
        public new void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    SetElement(i, j, matrix[i, j]);
                }
            }
        }
        public byte[] GetWinningPositions(int lineNumber)
        {
            return new[] { (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 0] * 3), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 1] * 3 + 1), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 2] * 3 + 2) };
        }

        /// <summary>
        /// Daje dobitni element za liniju
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public new int GetWinningElementForLine(int line)
        {
            return Matrix[0, GlobalData.GameLineVegasHot[line - 1, 0] + 1];
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
            fakeReels[0] = new[] { 0, 3, 3, 3, 1, 8, 8, 8, 2, 2, 0, 7, 7, 7, 1, 4, 4, 4, 0, 6, 6, 6, 1, 5, 5, 5 };
            fakeReels[1] = new[] { 1, 7, 7, 7, 0, 2, 2, 6, 6, 6, 1, 4, 4, 4, 0, 5, 5, 5, 1, 8, 8, 8, 0, 3, 3, 3 };
            fakeReels[2] = new[] { 0, 5, 5, 5, 1, 6, 6, 6, 0, 4, 4, 4, 1, 2, 2, 3, 3, 3, 0, 8, 8, 8, 1, 7, 7, 7 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            return new[] { 0, 0, WinForTopHot5[id] };
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.61,
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
            var lines = new HelpLineConfigV3[5];
            for (var i = 0; i < 5; i++)
            {
                var pos = new int[3];
                for (var j = 0; j < 3; j++)
                {
                    pos[j] = GlobalData.GameLineVegasHot[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
