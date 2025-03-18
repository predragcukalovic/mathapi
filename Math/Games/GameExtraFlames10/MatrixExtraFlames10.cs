using MathBaseProject.BaseMathData;
using MathBaseProject.StructuresV3;
using RNGUtils.RandomData;
using System;

namespace GameExtraFlames10
{
    public class MatrixExtraFlames10 : Matrix
    {
        private static readonly int[][][] _Reels =
        {
            new[]
            {
                new[]{8, 8, 8, 7, 7, 7, 5, 5, 5, 3, 3, 3, 6, 6, 6, 1, 1, 1, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 1, 1, 1, 6, 6, 6, 8, 8, 8, 6, 6, 6, 7, 7, 7, 5, 5, 5, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 4, 4, 4, 2, 2, 2, 7, 7, 7, 5, 5, 5, 8, 8, 8, 3, 3, 3, 1, 1, 1},
                new[]{7, 7, 7, 5, 5, 5, 8, 8, 8, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 7, 7, 7, 8, 8, 8, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 3, 3, 3, 5, 5, 5, 1, 1, 1, 4, 4, 4, 8, 8, 8},
                new[]{6, 6, 6, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 3, 3, 3, 8, 8, 8, 2, 2, 2, 5, 5, 5, 6, 6, 6, 4, 4, 4, 5, 5, 5, 4, 4, 4, 8, 8, 8, 7, 7, 7, 6, 6, 6, 2, 2, 2, 1, 1, 1, 3, 3, 3, 6, 6, 6, 3, 3, 3, 8, 8, 8, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2},
                new[]{7, 7, 7, 7, 7, 7, 1, 1, 1, 3, 3, 3, 8, 8, 8, 3, 3, 3, 4, 4, 4, 7, 7, 7, 6, 6, 6, 5, 5, 5, 8, 8, 8, 1, 1, 1, 8, 8, 8, 4, 4, 4, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 6, 1, 1, 1, 3, 3, 3, 8, 8, 8, 4, 4, 4, 6, 6, 6, 7, 7, 7, 2, 2, 2, 8, 8, 8, 5, 5, 5, 4, 4, 4, 3, 3, 3, 7, 7, 7},
                new[]{5, 5, 5, 5, 5, 5, 4, 4, 4, 7, 7, 7, 6, 6, 6, 2, 2, 2, 8, 8, 8, 5, 5, 5, 7, 7, 7, 6, 6, 6, 4, 4, 4, 2, 2, 2, 8, 8, 8, 6, 6, 6, 2, 2, 2, 6, 6, 6, 5, 5, 5, 4, 4, 4, 8, 8, 8, 1, 1, 1, 5, 5, 5, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4}
            },
            new[]
            {
                new[]{8, 8, 8, 7, 7, 7, 5, 5, 5, 3, 3, 3, 6, 6, 6, 1, 1, 1, 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 1, 1, 1, 6, 6, 6, 8, 8, 8, 6, 6, 6, 7, 7, 7, 5, 5, 5, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 4, 4, 4, 2, 2, 2, 7, 7, 7, 5, 5, 5, 8, 8, 8, 3, 3, 3, 1, 1, 1},
                new[]{7, 7, 7, 5, 5, 5, 8, 8, 8, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 7, 7, 7, 8, 8, 8, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 3, 3, 3, 5, 5, 5, 1, 1, 1, 4, 4, 4, 8, 8, 8},
                new[]{6, 6, 6, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 3, 3, 3, 8, 8, 8, 2, 2, 2, 5, 5, 5, 6, 6, 6, 4, 4, 4, 5, 5, 5, 4, 4, 4, 8, 8, 8, 7, 7, 7, 6, 6, 6, 2, 2, 2, 1, 1, 1, 3, 3, 3, 6, 6, 6, 3, 3, 3, 8, 8, 8, 6, 6, 6, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 2, 2, 2},
                new[]{7, 7, 7, 7, 7, 7, 1, 1, 1, 3, 3, 3, 8, 8, 8, 3, 3, 3, 4, 4, 4, 7, 7, 7, 6, 6, 6, 5, 5, 5, 8, 8, 8, 1, 1, 1, 8, 8, 8, 4, 4, 4, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 6, 1, 1, 1, 3, 3, 3, 8, 8, 8, 4, 4, 4, 6, 6, 6, 7, 7, 7, 2, 2, 2, 8, 8, 8, 5, 5, 5, 4, 4, 4, 3, 3, 3, 7, 7, 7},
                new[]{5, 5, 5, 5, 5, 5, 4, 4, 4, 7, 7, 7, 6, 6, 6, 2, 2, 2, 8, 8, 8, 5, 5, 5, 7, 7, 7, 6, 6, 6, 4, 4, 4, 2, 2, 2, 8, 8, 8, 6, 6, 6, 2, 2, 2, 6, 6, 6, 5, 5, 5, 4, 4, 4, 8, 8, 8, 1, 1, 1, 5, 5, 5, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4}
            }
        };

        #region Public properties

        public static int[] PlayLines = { 10 };

        public static readonly int[,] GameLineExtraFlames =
        {
            {1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0},
            {2, 2, 2, 2, 2},
            {0, 1, 2, 1, 0},
            {2, 1, 0, 1, 2},
            {2, 2, 1, 0, 0},
            {0, 0, 1, 2, 2},
            {1, 0, 0, 0, 1},
            {1, 2, 2, 2, 1},
            {0, 1, 1, 1, 0}
        };

        public static readonly int[,] WinForLinesExtraFlames10 =
        {
            {0, 0, 0, 0, 0},
            {0, 0, 50, 500, 2500},
            {0, 0, 20, 100, 500},
            {0, 0, 20, 100, 500},
            {0, 0, 10, 40, 200},
            {0, 0, 10, 40, 200},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 10, 20, 100},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}
        };

        public static int WinForWildExtraFlames10 = 10;

        #endregion

        /// <summary>
        /// uzima liniju iz matrice
        /// </summary>
        /// <param name="lineNumber">broj linije, 1 -- 10</param>
        /// <returns>vraća liniju pod datim brojem</returns>
        public LineExtraFlames10 GetLineExtraFlames(int lineNumber)
        {
            try
            {
                var line = new LineExtraFlames10();
                for (var i = 0; i < 5; i++)
                {
                    line.SetElement(i, GetElement(i, GameLineExtraFlames[lineNumber - 1, i]));
                }
                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int[,] GetMatixArray()
        {
            var reelsSet = 0;
            if (SoftwareRng.Next() < 0.1897512)
            {
                reelsSet = 1;
            }
            var mat = new int[5, 5];
            for (var i = 0; i < 5; i++)
            {
                var l = _Reels[reelsSet][i].Length;
                var p = SoftwareRng.Next(l);
                for (var j = 0; j < 5; j++)
                {
                    mat[i, j] = _Reels[reelsSet][i][(p + j) % l];
                }
            }
            return mat;
        }

        #region V3 structs

        /// <summary>
        /// Vraća lažne rilove koji se koriste samo za prikaz okretanja
        /// </summary>
        /// <returns></returns>
        public static int[][] GetFakeReels()
        {
            var fakeReels = new int[5][];
            fakeReels[0] = new[] { 4, 4, 4, 3, 3, 3, 5, 5, 5, 6, 6, 6, 1, 1, 1, 6, 6, 6, 8, 8, 8, 6, 6, 6, 7, 7, 7, 5, 5, 5, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 1, 1, 6, 6, 6, 4, 4, 4, 2, 2, 2, 7, 7, 7, 5, 5, 5, 8, 8, 8, 3, 3, 3, 1, 1, 1 };
            fakeReels[1] = new[] { 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 7, 7, 7, 8, 8, 8, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 7, 7, 7, 2, 2, 2, 4, 4, 4, 6, 6, 6, 8, 8, 8, 3, 3, 3, 5, 5, 5, 1, 1, 1, 4, 4, 4, 8, 8, 8 };
            fakeReels[2] = new[] { 3, 3, 3, 8, 8, 8, 2, 2, 2, 5, 5, 5, 6, 6, 6, 4, 4, 4, 5, 5, 5, 4, 4, 4, 8, 8, 8, 7, 7, 7, 6, 6, 6, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 6, 6, 6, 3, 3, 3, 8, 8, 8, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2 };
            fakeReels[3] = new[] { 7, 7, 7, 6, 6, 6, 5, 5, 5, 8, 8, 8, 1, 1, 1, 8, 8, 8, 4, 4, 4, 3, 3, 3, 7, 7, 7, 1, 1, 1, 6, 6, 6, 1, 1, 1, 3, 3, 3, 8, 8, 8, 4, 4, 4, 6, 6, 6, 7, 7, 7, 2, 2, 2, 8, 8, 8, 5, 5, 5, 4, 4, 4, 3, 3, 3, 7, 7, 7 };
            fakeReels[4] = new[] { 5, 5, 5, 5, 5, 5, 4, 4, 4, 7, 7, 7, 6, 6, 6, 2, 2, 2, 8, 8, 8, 6, 6, 6, 2, 2, 2, 6, 6, 6, 5, 5, 5, 4, 4, 4, 8, 8, 8, 1, 1, 1, 5, 5, 5, 3, 3, 3, 6, 6, 6, 7, 7, 7, 5, 5, 5, 2, 2, 2, 4, 4, 4 };

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
                return new[] { WinForWildExtraFlames10, 0, 0, 0, 0 };
            }
            var coefficients = new int[5];
            for (var i = 0; i < 5; i++)
            {
                coefficients[i] = WinForLinesExtraFlames10[id, i];
            }
            return coefficients;
        }

        public static HelpConfigV3<object> GetHelpConfigV3()
        {
            var helpV3 = new HelpConfigV3<object>
            {
                rtp = (decimal?)95.8,
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
            for (var i = 0; i < 10; i++)
            {
                var pos = new int[5];
                for (var j = 0; j < 5; j++)
                {
                    pos[j] = GameLineExtraFlames[i, j];
                }
                lines[i] = new HelpLineConfigV3 { id = i, positions = pos };
            }

            return lines;
        }

        #endregion
    }
}
