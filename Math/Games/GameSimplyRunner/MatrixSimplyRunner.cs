using MathBaseProject.StructuresV3;
using RNGUtils.RandomData;

namespace GameSimplyRunner
{
    public class MatrixSimplyRunner
    {
        #region Private fields

        protected readonly int[,] Matrix;

        #endregion

        #region Public fileds

        public static readonly int[,,] GameLinesSimplyRunner =
        {
            {{0,2},{1,2},{2,2}},
            {{0,1},{1,1},{2,1}},
            {{0,3},{1,3},{2,3}},
            {{0,3},{1,2},{2,1}},
            {{0,1},{1,2},{2,3}},
            {{1,2},{2,2},{3,2}},
            {{1,1},{2,1},{3,1}},
            {{1,3},{2,3},{3,3}},
            {{1,3},{2,2},{3,1}},
            {{1,1},{2,2},{3,3}},
            {{2,2},{3,2},{4,2}},
            {{2,1},{3,1},{4,1}},
            {{2,3},{3,3},{4,3}},
            {{2,3},{3,2},{4,1}},
            {{2,1},{3,2},{4,3}},
            {{3,2},{4,2},{5,2}},
            {{3,1},{4,1},{5,1}},
            {{3,3},{4,3},{5,3}},
            {{3,3},{4,2},{5,1}},
            {{3,1},{4,2},{5,3}}
        };

        private static readonly int[][] _Reels =
        {
            new[] { 5, 5, 14, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4 },
            new[] { 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 14, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4 },
            new[] { 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 }
        };

        private static readonly int[][] _ReelsU =
        {
            new[] { 5, 5, 14, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 5, 0, 0, 0, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4, 5, 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4 },
            new[] { 5, 5, 5, 5, 0, 0, 0, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5, 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 14, 5, 0, 0, 0, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5, 5, 5, 14, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 6, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 },
            new[] { 5, 5, 5, 0, 0, 0, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4, 5, 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 6, 3, 6, 3, 6, 12, 8, 8, 8, 4, 8, 4 },
            new[] { 5, 5, 5, 5, 1, 1, 7, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 8, 4, 4, 5 }
        };

        public static int[] PlayLines = { 5, 10, 15, 20 };

        #endregion

        #region Constructors

        public MatrixSimplyRunner()
        {
            Matrix = new int[6, 5];
        }

        #endregion

        #region Public methods

        public LineSimplyRunner GetLine(int lineNumber)
        {
            var line = new LineSimplyRunner();
            line.SetElement(0, Matrix[GameLinesSimplyRunner[lineNumber - 1, 0, 0], GameLinesSimplyRunner[lineNumber - 1, 0, 1]]);
            line.SetElement(1, Matrix[GameLinesSimplyRunner[lineNumber - 1, 1, 0], GameLinesSimplyRunner[lineNumber - 1, 1, 1]]);
            line.SetElement(2, Matrix[GameLinesSimplyRunner[lineNumber - 1, 2, 0], GameLinesSimplyRunner[lineNumber - 1, 2, 1]]);
            return line;
        }

        public static int[,] GetMatrixArray(bool ultra = false)
        {
            var reelSet = _Reels;
            if (ultra && SoftwareRng.Next() < 0.824655)
            {
                reelSet = _ReelsU;
            }
            var mat = new int[6, 5];
            for (var i = 0; i < 6; i++)
            {
                var l = _Reels[i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = reelSet[i][(p + j) % l];
                }
            }
            return mat;
        }

        public void FromMatrixArray(int[,] matrix)
        {
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Matrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Daje element na poziciji [i,j].
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int GetElement(int i, int j)
        {
            return Matrix[i, j];
        }

        public int GetWinningElementForLine(int lineNumber)
        {
            return GetLine(lineNumber).GetWinningElement();
        }

        public int CalculateWinLine(int lineNumber)
        {
            return GetLine(lineNumber).CalculateLineWin();
        }

        /// <summary>
        /// Pozicije dobitnih elemenata u matrici.
        /// </summary>
        /// <param name="array">Niz u koji se pamte pozicije</param>
        /// <param name="line">Broj linije</param>
        public static void CreateWinningLinePositions(ref byte[] array, int line)
        {
            var start = GameLinesSimplyRunner[line - 1, 0, 0];
            for (var i = 0; i < 3; i++)
            {
                array[i] = (byte)((GameLinesSimplyRunner[line - 1, i, 1] - 1) * 6 + start + i);
            }
        }

        #endregion

        #region V3 Structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 5, 5, 14, 5, 1, 1, 7, 7, 7, 2, 2, 6, 6, 6, 3, 3, 8, 8, 8, 4, 4, 5 };
            fakeReels[1] = new[] { 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 3, 6, 3, 6, 12, 8, 8, 4, 8, 4 };
            fakeReels[2] = new[] { 5, 5, 5, 1, 1, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 4, 4, 5 };
            fakeReels[3] = new[] { 5, 5, 14, 5, 1, 7, 7, 7, 2, 2, 6, 6, 6, 3, 3, 8, 8, 8, 4, 4, 5 };
            fakeReels[4] = new[] { 5, 5, 1, 5, 1, 7, 16, 7, 2, 7, 2, 6, 6, 3, 6, 3, 6, 12, 8, 8, 4, 8, 4 };
            fakeReels[5] = new[] { 5, 5, 5, 1, 1, 7, 7, 7, 2, 2, 6, 6, 15, 6, 3, 3, 8, 8, 8, 4, 4, 5 };

            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            var coefficients = new int[1];
            coefficients[0] = LineSimplyRunner.WinSimplyRunner[id];
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
            var lines = new HelpLineConfigV3[10];
            for (var i = 0; i < 20; i++)
            {
                var pos = new[] { -1, -1, -1, -1, -1, -1 };
                for (var j = 0; j < 3; j++)
                {
                    pos[GameLinesSimplyRunner[i, j, 0]] = GameLinesSimplyRunner[i, j, 1] - 1;
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
