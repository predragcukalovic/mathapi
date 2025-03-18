using MathBaseProject.StructuresV3;
using MathForGames.BasicGameData;
using RNGUtils.RandomData;

namespace GameJokerTripleDouble
{
    public class MatrixJokerTripleDouble
    {
        #region Private fields

        protected int[,] Matrix;
        private static readonly int[][][] _Reels =
        {
            new[]{
                new[]{1,1,1,6,6,7,7,0,2,2,2,7,7,3,3,3,0,5,5,5,0,6,6,2,2,2,3,3,4,4,6,6,7,7,5,5,4,4,2,2,0,7,7,5,5,6,6,1,1,1,7,7,6,6,4,4,4,0,7,7,6,6,7,7,7,4,4,4,6,6,6,7,7,4,4,3,3,2,2,2,6,6,7,7,3,3,5,5,7,7,7,6,6,5,5,7,7,7,2,2,6,6,7,7,3,3,3,7,7,7,4,4,5,5,7,7,7,6,6,6,4,4,4,5,5,5,7,7,4,4,3,3,7,7,5,5,5,2,2,6,6,6,3,3,5,5,4,4,7,7,3,3,6,6,6},
                new[]{7,7,6,6,6,0,4,4,4,3,3,7,7,4,4,6,6,7,7,3,3,3,2,2,2,0,7,7,1,1,5,5,0,3,3,3,6,6,6,5,5,4,4,0,1,1,2,2,6,6,6,4,4,7,7,7,4,4,2,2,1,1,1,5,5,3,3,6,6,7,7,7,3,3,6,6,4,4,5,5,7,7,7,6,6,7,7,3,3,5,5,4,4,3,3,6,6,6,5,5,5,6,6,3,3,6,6,4,4,6,6,7,7,4,4,2,2,5,5,5,6,6,4,4,4,5,5,6,6,6,3,3,5,5,5,6,6,4,4,2,2,2,6,6,7,7,3,3,4,4,1,1,7,7,4,4,2,2,2,3,3,7,7,6,6,4,4,6,6},
                new[]{4,4,4,0,6,6,6,7,7,7,0,3,3,3,6,6,7,7,5,5,4,4,4,7,7,6,6,5,5,2,2,5,5,7,7,7,1,1,1,7,7,2,2,2,6,6,3,3,3,0,5,5,5,1,1,1,4,4,4,2,2,5,5,5,6,6,6,0,7,7,7,7,7,4,4,5,5,6,6,4,4,2,2,7,7,2,2,5,5,7,7,6,6,6,5,5,5,4,4,3,3,3,7,7,4,4,6,6,2,2,7,7,5,5,3,3,7,7,5,5,6,6,7,7,3,3,5,5,3,3,7,7,6,6,5,5,2,2,4,4,6,6,5,5,2,2,7,7,2,2,5,5,6,6,4,4,5,5,2,2}
            },
            new[]{
                new[]{1,1,1,7,7,7,0,2,2,2,3,3,3,0,5,5,5,1,1,1,0,2,2,2,6,6,6,3,3,3,4,4,4,0,6,6,6,7,7,7,4,4,4,2,2,2,0,5,5,5,6,6,6,0,1,1,1,4,4,4,0,5,5,5,7,7,7},
                new[]{1,1,1,3,3,3,4,4,4,7,7,7,3,3,3,1,1,1,7,7,7,4,4,4,1,1,1,4,4,4,3,3,3,7,7,7},
                new[]{2,2,2,5,5,5,6,6,6,5,5,5,2,2,2,6,6,6}
            },
            new[]{
                new[]{2,2,2,5,5,5,6,6,6,5,5,5,2,2,2,6,6,6},
                new[]{1,1,1,2,2,2,0,4,4,4,0,5,5,5,6,6,6,0,1,1,1,7,7,7,3,3,3,0,5,5,5,2,2,2,3,3,3,4,4,4,0,7,7,7,6,6,6,2,2,2,0,4,4,4,1,1,1,0,3,3,3,6,6,6,7,7,7,0},
                new[]{1,1,1,3,3,3,4,4,4,7,7,7,3,3,3,1,1,1,7,7,7,4,4,4,1,1,1,4,4,4,3,3,3,7,7,7}
            },
            new[]{
                new[]{1,1,1,3,3,3,4,4,4,7,7,7,3,3,3,1,1,1,7,7,7,4,4,4,1,1,1,4,4,4,3,3,3,7,7,7},
                new[]{2,2,2,5,5,5,6,6,6,5,5,5,2,2,2,6,6,6},
                new[]{4,4,4,0,6,6,6,7,7,7,1,1,1,2,2,2,0,3,3,3,4,4,4,0,2,2,2,5,5,5,7,7,7,1,1,1,0,2,2,2,6,6,6,3,3,3,0,5,5,5,1,1,1,4,4,4,0,5,5,5,6,6,6,0,7,7,7,0}
            }
        };

        private static readonly int[][] _RespinReels =
        {
            new[]{1,1,1,7,7,7,0,2,2,2,7,7,7,3,3,3,0,5,5,5,2,2,2,6,6,6,3,3,3,4,4,4,0,6,6,6,7,7,7,4,4,4,0,5,5,5,6,6,6,4,4,4,7,7,7,4,4,4,7,7,7,5,5,5,7,7,7},
            new[]{7,7,7,6,6,6,0,4,4,4,3,3,3,2,2,2,0,7,7,7,1,1,1,5,5,5,0,3,3,3,7,7,7,6,6,6,5,5,5,4,4,4,6,6,6,3,3,3,7,7,7,6,6,6,7,7,7,2,2,2,1,1,1},
            new[]{4,4,4,6,6,6,7,7,7,2,2,2,0,3,3,3,4,4,4,0,2,2,2,5,5,5,7,7,7,1,1,1,5,5,5,6,6,6,3,3,3,5,5,5,7,7,7,5,5,5,6,6,6,0,7,7,7,3,3,3}
        };

        private static readonly double[] _ReelProbs = { 0.46222, 0.17926, 0.17926, 0.17926 };

        #endregion

        #region Public fields

        public static readonly int[] WinForJokerTripleDouble = { 50, 50, 15, 10, 8, 6, 4, 2 };
        public static int[] PlayLines = { 1 };

        #endregion

        #region Constructor

        public MatrixJokerTripleDouble()
        {
            Matrix = new int[3, 5];
        }

        #endregion

        #region Public fields

        public int GetLineWin(int lineNumber, out int winElem, out bool wild)
        {
            var line = new[] { Matrix[0, GlobalData.GameLineVegasHot[lineNumber - 1, 0] + 1], Matrix[1, GlobalData.GameLineVegasHot[lineNumber - 1, 1] + 1], Matrix[2, GlobalData.GameLineVegasHot[lineNumber - 1, 2] + 1] };
            wild = line[0] * line[1] * line[2] == 0;
            winElem = line[0];
            if (winElem == 0)
            {
                winElem = line[1];
            }
            if (winElem == 0)
            {
                winElem = line[2];
            }
            if (line[1] != winElem && line[1] != 0)
            {
                return 0;
            }
            if (line[2] != winElem && line[2] != 0)
            {
                return 0;
            }
            return WinForJokerTripleDouble[winElem];
        }

        public int GetElement(int i, int j)
        {
            return Matrix[i, j];
        }

        public void SetElement(int i, int j, int elem)
        {
            Matrix[i, j] = elem;
        }

        public byte[] GetWinningPositions(int lineNumber)
        {
            return new byte[] { (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 0] * 3), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 1] * 3 + 1), (byte)(GlobalData.GameLineVegasHot[lineNumber - 1, 2] * 3 + 2) };
        }

        public bool DoubleWin()
        {
            var winElem = -1;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        if (winElem == -1)
                        {
                            winElem = Matrix[i, j];
                        }
                        else if (Matrix[i, j] != winElem)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static int[,] GetMatixArray(bool gratisGame)
        {
            var mat = new int[3, 5];
            var reelsSet = -1;
            if (!gratisGame)
            {
                var sum = 0.0;
                var rnd = SoftwareRng.Next();
                var n = _ReelProbs.Length;
                for (var i = 0; i < n; i++)
                {
                    sum += _ReelProbs[i];
                    if (rnd < sum)
                    {
                        reelsSet = i;
                        break;
                    }
                }
            }
            var reels = gratisGame ? _RespinReels : _Reels[reelsSet];
            for (var i = 0; i < 3; i++)
            {
                var l = reels[i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = reels[i][(p + j) % l];
                }
            }
            return mat;
        }

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

        #endregion

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[3][];
            fakeReels[0] = new[] { 6, 6, 6, 1, 1, 1, 0, 7, 7, 7, 3, 3, 3, 0, 4, 4, 4, 0, 5, 5, 5, 6, 6, 6, 2, 2, 2, 3, 3, 5, 5, 4, 4, 1, 1, 0, 3, 3, 2, 2, 0 };
            fakeReels[1] = new[] { 3, 3, 4, 4, 4, 0, 5, 5, 5, 0, 7, 7, 7, 6, 6, 6, 2, 2, 2, 7, 7, 0, 1, 1, 1, 5, 5, 1, 1, 6, 6, 3, 3, 4, 4, 2, 2, 0 };
            fakeReels[2] = new[] { 5, 5, 5, 0, 1, 1, 1, 6, 6, 6, 0, 7, 7, 7, 3, 3, 3, 5, 5, 0, 2, 2, 2, 4, 4, 4, 1, 1, 6, 6, 7, 7, 5, 5, 2, 2, 3, 3, 0 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReelsRespin()
        {
            var fakeReels = new int[3][];
            fakeReels[0] = new[] { 6, 6, 6, 1, 1, 1, 0, 7, 7, 7, 3, 3, 3, 0, 4, 4, 4, 0, 5, 5, 5, 6, 6, 6, 2, 2, 2, 3, 3, 5, 5, 4, 4, 1, 1, 0, 3, 3, 2, 2, 0 };
            fakeReels[1] = new[] { 3, 3, 4, 4, 4, 0, 5, 5, 5, 0, 7, 7, 7, 6, 6, 6, 2, 2, 2, 7, 7, 0, 1, 1, 1, 5, 5, 1, 1, 6, 6, 3, 3, 4, 4, 2, 2, 0 };
            fakeReels[2] = new[] { 5, 5, 5, 0, 1, 1, 1, 6, 6, 6, 0, 7, 7, 7, 3, 3, 3, 5, 5, 0, 2, 2, 2, 4, 4, 4, 1, 1, 6, 6, 7, 7, 5, 5, 2, 2, 3, 3, 0 };
            return fakeReels;
        }

        /// <summary>
        /// Vraća niz koeficijenata za id simbola.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetSymbolCoefficients(int id)
        {
            return new[] { 0, 0, WinForJokerTripleDouble[id] };
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)96.5,
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
