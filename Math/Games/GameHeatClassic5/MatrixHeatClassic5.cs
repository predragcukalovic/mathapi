using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using MathForGames.GameVegasHot;
using RNGUtils.RandomData;

namespace GameHeatClassic5
{
    public class MatrixHeatClassic5 : MatrixVegasHot
    {
        #region Constructors

        public MatrixHeatClassic5()
        {
            Matrix = new int[3, 5];
        }

        #endregion

        public static int[] PlayLines = { 5 };

        #region Private properties

        private static readonly int[][] _Reels =
        {
            new[] { 8, 8, 8, 8, 2, 2, 5, 5, 5, 5, 5, 3, 3, 1, 4, 4, 6, 6, 6, 6, 6, 7, 5, 7, 7, 7, 7, 7, 8, 6, 0, 8 },
            new[] { 8, 8, 8, 2, 5, 2, 5, 5, 5, 5, 7, 3, 3, 4, 1, 4, 6, 6, 6, 8, 6, 8, 0, 6, 7, 7, 7, 7 },
            new[] { 8, 8, 8, 8, 2, 2, 7, 5, 5, 5, 5, 5, 1, 4, 4, 6, 6, 6, 6, 8, 3, 5, 3, 7, 7, 7, 7, 6, 0, 6, 8 }
        };

        #endregion

        #region Private methods

        /// <summary>
        /// Daje liniju iz matrice.
        /// </summary>
        /// <param name="numberOfLine"></param>
        /// <returns></returns>
        private LineHeatClassic5 GetLine(int numberOfLine)
        {
            var line = new LineHeatClassic5();
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
        public override int CalculateWinOfLine(int numberOfLine)
        {
            var line = GetLine(numberOfLine);
            return line.CalculateLineWin();
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

        public static int[,] GetMatixArray()
        {
            var mat = new int[3, 5];
            for (var i = 0; i < 3; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[i][(p + j) % l];
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
                    Matrix[i, j] = matrix[i, j];
                }
            }
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
            fakeReels[0] = new[] { 8, 8, 8, 2, 2, 5, 5, 5, 3, 3, 1, 4, 4, 6, 6, 6, 7, 5, 7, 7, 7, 8, 6, 0 };
            fakeReels[1] = new[] { 4, 6, 6, 6, 7, 5, 7, 7, 7, 8, 6, 0, 8, 8, 8, 2, 2, 5, 5, 5, 3, 3, 1, 4 };
            fakeReels[2] = new[] { 5, 5, 5, 3, 3, 1, 4, 4, 6, 6, 6, 7, 5, 7, 7, 7, 8, 6, 0, 8, 8, 8, 2, 2 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            return new[] { 0, 0, LineHeatClassic5.WinForLinesHeatClassic[id] };
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.95,
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
